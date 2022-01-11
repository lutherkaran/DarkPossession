using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PossessionController, IFeatures, IPossessable
{

    float fSpeed = 5f;
    float fHorizontal = 0f;
    public float fJumpMultiplier = 40f;
    Vector2 Pos = Vector2.zero;
    public bool bPlayerCanJump = false;
    bool bPlayerIsPossessed = false;
    public bool bPlayerIsAlive = false;
    private bool bDoJump = false;

    public Player()
    {

    }
    public void Initialize()
    {
    }

    public void PostInitialize()
    {
        controlledObj = this.GetComponent<IPossessable>();
        if (controlledObj == this.GetComponent<IPossessable>())
        {
            Debug.Log("PLAYER IS ALREADY POSSESSED");
            bPlayerIsPossessed = true;
        }

    }
    public void Refresh(float _deltaTime)
    {
        if (bPlayerIsPossessed)
        {
            Move(_deltaTime);
            doJump();
        }
        PlayerFeatures();
    }
    public void PhysicsRefresh(float _fixedDeltaTime)
    {
        if (bDoJump) { Jump(_fixedDeltaTime); }
    }
    public void Move(float _deltaTime)
    {
        fHorizontal = Input.GetAxis("Horizontal");
        Pos = transform.position;
        Pos.x += fHorizontal * fSpeed * _deltaTime;
        transform.position = Pos;
    }

    private void doJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !bDoJump)
        {
            bDoJump = true;
        }

    }
    public void Jump(float _fixedDeltaTime)
    {
        if (bPlayerCanJump)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * fJumpMultiplier * _fixedDeltaTime, ForceMode2D.Impulse);
        }
        bDoJump = false;
        bPlayerCanJump = false;
    }


    public void Attack()
    {

    }


    public void possess()
    {
        bPlayerIsPossessed = true;
        PlayerManager.instance.gPlayer.bPlayerCanJump = true;

    }

    public void unpossess()
    {
        bPlayerIsPossessed = false;
        PlayerManager.instance.gPlayer.bPlayerCanJump = false;
    }
    public void PlayerFeatures()
    {
        if (bPlayerIsAlive)
        {
            /*gPlayer.Attack();*/
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (bPlayerIsPossessed == true)
                {
                    Debug.Log("POSSESSING ENEMY....");
                    IPossessable IenemyPossessable = EnemyManager.instance.gEnemy.GetComponent<IPossessable>();

                    if (IenemyPossessable != null)
                    {
                        possess(IenemyPossessable);
                    }
                    bPlayerIsPossessed = false;
                }
                else if (bPlayerIsPossessed == false)
                {
                    Debug.Log("POSSESSING PLAYER....");
                    IPossessable IplayerPossessable = this.GetComponent<IPossessable>();
                    if (IplayerPossessable != null)
                    {
                        Debug.Log("PLAYER POSSESSED....");
                        possess(IplayerPossessable);
                    }
                    bPlayerIsPossessed = true;
                }
            }

        }

        else { Debug.Log("P L A Y E R   D I E D"); }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bPlayerCanJump = true;
        }
    }
}
