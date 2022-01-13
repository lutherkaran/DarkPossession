using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 vBulletPos;
    public float fBulletSpeed = 20000f;

    bool bIsFired = false;
    Player player;
    int iMaxBullet = 0;
    public GameObject go;


    public void Initialize()
    {

        go = this.gameObject;
        iMaxBullet = 0;

    }

    public void PhysicsRefresh(float _fixedDeltaTime)
    {

    }

    public void PostInitialize()
    {
        vBulletPos = this.transform.position;
        player = PlayerManager.instance.gPlayer;
    }

    public void Refresh(float _deltaTime)
    {


    }

    public void Attack(Vector3 _vPos)
    {
        this.transform.position = _vPos;
        if (player)
        {
            if (iMaxBullet < 1)
            {
                iMaxBullet++;
                if (!bIsFired)
                {
                    go.gameObject.GetComponent<Rigidbody2D>().AddForce((player.gameObject.transform.position - _vPos).normalized * Time.fixedDeltaTime * fBulletSpeed);
                    bIsFired = true;
                }
                if (!go.activeInHierarchy)
                {
                    go.SetActive(true);
                    go.GetComponent<Rigidbody2D>().AddForce((player.gameObject.transform.position - _vPos).normalized * Time.fixedDeltaTime * fBulletSpeed);
                }
                else { bIsFired = false; }
            }
        }
        this.gameObject.transform.SetParent(null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.collider.gameObject.tag)
        {
            case "Player":
                IHittable hitPlayer = collision.collider.gameObject.GetComponent<IHittable>();
                if (hitPlayer != null)
                {
                    float _fDamage = 50f;
                    hitPlayer.Damage(_fDamage);
                    ResetBulletPosition();
                }
                break;
            case "Enemy":
                IHittable hitEnemy = collision.collider.gameObject.GetComponent<IHittable>();
                if (hitEnemy != null)
                {
                    float _fDamage = 50f;
                    hitEnemy.Damage(_fDamage);
                    ResetBulletPosition();
                }
                break;

            case "Wall":

                LayerMask mask = LayerMask.GetMask("Wall");

                if (go.GetComponent<Collider2D>().IsTouchingLayers(mask.value))
                {
                    ResetBulletPosition();
                }
                break;
        }
    }

    private void ResetBulletPosition()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.SetParent(EnemyManager.instance.gEnemy.transform);
        vBulletPos = EnemyManager.instance.gEnemy.transform.position;
        this.gameObject.transform.position = vBulletPos;
        bIsFired = false;
        iMaxBullet = 0;
    }

    public void FireBullet(Vector2 _currRot, Vector2 _vPos)
    {
        if (!bIsFired)
        {
            if (go != null)
            {
                ResetBulletPosition();
                go.SetActive(true);
                go.gameObject.GetComponent<Rigidbody2D>().AddForce(_currRot * Time.fixedDeltaTime * fBulletSpeed);
                bIsFired = true;
            }

            else
            {
                BulletManager.instance.BulletInstantiation(_vPos);
                go.GetComponent<Rigidbody2D>().AddForce(_currRot * Time.fixedDeltaTime * fBulletSpeed);
                bIsFired = true;
            }
        }

        this.gameObject.transform.SetParent(null);
    }
}
