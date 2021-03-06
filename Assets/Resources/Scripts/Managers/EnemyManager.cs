using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IUpdate
{

    private static EnemyManager Instance;
    private EnemyManager() { }
    public static EnemyManager instance { get { return Instance ?? (Instance = new EnemyManager()); } }
    public Vector2 vStartPos;
    public Enemy gEnemy;
    Transform tEnemyParent;
    bool bEnemyIsAlive = false;
    public enum enemyType { Boomer, Starter };
    Dictionary<enemyType, GameObject> dEnemyPrefabDict;

    public void Initialize()
    {
        Enemy();
    }

    private void Enemy()
    {
        vStartPos = new Vector2(-5, 1);
        gEnemy = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"), vStartPos, Quaternion.identity).AddComponent<Enemy>();
        tEnemyParent = GameObject.Find("EnemySpawnner").transform;
        gEnemy.gameObject.transform.SetParent(tEnemyParent);

        if (gEnemy) { bEnemyIsAlive = true; }
        else { bEnemyIsAlive = false; Debug.Log("E N E M Y   D I E D"); }
    }

    public void PhysicsRefresh(float _fixedDeltaTime)
    {
        gEnemy.PhysicsRefresh(_fixedDeltaTime);
    }

    public void PostInitialize()
    {
        gEnemy.PostInitialize();
    }


    public void Refresh(float _deltaTime)
    {
        gEnemy.Refresh(_deltaTime);
        EnemyFeatures();
    }

    private void EnemyFeatures()
    {
        if (gEnemy.enabled)
        {
            if (gEnemy.gameObject != null)
            {
                //
            }
            else
            {//Debug.Log("E N E M Y   D I E D");
            }
        }
        else
        { //Debug.Log("Enemy Script is disabled");
        }

    }
}

