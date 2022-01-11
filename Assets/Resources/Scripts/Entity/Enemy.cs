using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IFeatures, IPossessable
{
    Player player;
    Rigidbody2D enemyRb;
    float enemySpeed = 250f;
    Vector2 movDir = new Vector2(1, 0);

    public bool bEnemyIsPossessed = false;
    public Transform tInitialTransform;
    public float fRange = 2.5f;

    public void Initialize()
    {
        tInitialTransform = this.transform;
    }

    public void PostInitialize()
    {
        player = PlayerManager.instance.gPlayer;
        tInitialTransform = this.transform;
        enemyRb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public void Refresh(float _deltaTime)
    {
        if (!bEnemyIsPossessed) { Move(_deltaTime); }
        else { LookAt(); MoveByPlayer(_deltaTime); }
    }

    private void MoveByPlayer(float _deltaTime)
    {
        float fHorizontal = 0f;
        float fSpeed = 10f;
        fHorizontal = Input.GetAxis("Horizontal");
        Vector2 pos = this.transform.position;
        pos.x += fHorizontal * fSpeed * _deltaTime;
        this.transform.position = pos;
    }

    public void PhysicsRefresh(float _fixedDeltaTime)
    {

    }
    public void Attack()
    {
        BulletManager.instance.bullet.Attack(this.transform.position);
    }


    private void CheckWall()
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        if (Physics2D.Raycast(transform.position, movDir, fRange, mask.value))
        {
            movDir *= -1;

        }
    }

    private void LookAt()
    {
        Vector2 currRot = /*player.gameObject.transform.position*/ Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position;
        //currRot.Normalize();
        currRot = currRot.normalized;
        float fRotZ = Mathf.Atan2(currRot.y, currRot.x) * Mathf.Rad2Deg;
        if (fRotZ <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, fRotZ * -1);
        }
        else { transform.rotation = Quaternion.Euler(0, 0, fRotZ); }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BulletManager.instance.bullet.FireBullet(currRot, this.transform.position);
        }
    }

    public void possess()
    {
        bEnemyIsPossessed = true;
        Debug.Log("Enemy is Possessed");
    }

    public void unpossess()
    {
        bEnemyIsPossessed = false;
        this.enabled = true;
        this.gameObject.transform.rotation = tInitialTransform.rotation;
        Debug.Log("Enemy is Unpossessed");

    }

    public void Move(float _deltaTime)
    {
        enemyRb.AddForce(movDir * enemySpeed * _deltaTime);
        CheckWall();
    }

    public void Jump(float _fixedDeltaTime)
    {

    }
}
