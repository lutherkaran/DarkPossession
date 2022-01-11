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
            }
        }
        this.gameObject.transform.SetParent(null);
    }

    public bool BulletHit()
    {

        LayerMask mask = LayerMask.GetMask("Wall", "Player");

        if (go.GetComponent<Collider2D>().IsTouchingLayers(mask.value))
        {
            ResetBulletPosition();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetBulletPosition()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.SetParent(EnemyManager.instance.gEnemy.transform);
        vBulletPos = EnemyManager.instance.gEnemy.transform.position;
        this.gameObject.transform.position = vBulletPos;
        iMaxBullet = 0;
    }

    public void FireBullet(Vector2 _currRot, Vector2 _vPos)
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
            //GameObject.Instantiate(go, _vPos/*this.transform.position*/, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().AddForce(_currRot * Time.fixedDeltaTime * fBulletSpeed);
            bIsFired = true;
        }
        this.gameObject.transform.SetParent(null);
    }
}
