using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : IUpdate
{
    private static BulletManager Instance;
    private BulletManager() { }
    public static BulletManager instance { get { return Instance ?? (Instance = new BulletManager()); } }

    public Bullet bullet;
    public GameObject gBulletPrefab;

    public void Initialize()
    {
        gBulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        BulletInstantiation(EnemyManager.instance.gEnemy.transform.position);
        bullet.Initialize();
    }

    public void BulletInstantiation(Vector2 _vPos)
    {

        bullet = GameObject.Instantiate(gBulletPrefab/*Resources.Load<GameObject>("Prefabs/Bullet")*/, _vPos, Quaternion.identity).AddComponent<Bullet>();
        bullet.gameObject.SetActive(false);
    }

    public void PhysicsRefresh(float _fixedDeltaTime)
    {
        bullet.PhysicsRefresh(_fixedDeltaTime);
    }

    public void PostInitialize()
    {
        bullet.PostInitialize();
    }

    public void Refresh(float _deltaTime)
    {
        bullet.Refresh(_deltaTime);
    }

}
