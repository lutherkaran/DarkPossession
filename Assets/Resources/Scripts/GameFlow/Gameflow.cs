using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameflow : IUpdate
{
    private static Gameflow Instance;
    private Gameflow() { }
    public static Gameflow instance { get { return Instance ?? (Instance = new Gameflow()); } }



    public void Initialize()
    {
        PlayerManager.instance.Initialize();
        EnemyManager.instance.Initialize();
        BulletManager.instance.Initialize();
    }


    public void PhysicsRefresh(float _fixedDeltaTime)
    {
        PlayerManager.instance.PhysicsRefresh(_fixedDeltaTime);
        EnemyManager.instance.PhysicsRefresh(_fixedDeltaTime);
        BulletManager.instance.PhysicsRefresh(_fixedDeltaTime);
    }

    public void PostInitialize()
    {
        PlayerManager.instance.PostInitialize();
        EnemyManager.instance.PostInitialize();
        BulletManager.instance.PostInitialize();
    }

    public void Refresh(float _deltaTime)
    {
        PlayerManager.instance.Refresh(_deltaTime);
        EnemyManager.instance.Refresh(_deltaTime);
        BulletManager.instance.PhysicsRefresh(_deltaTime);
    }
}
