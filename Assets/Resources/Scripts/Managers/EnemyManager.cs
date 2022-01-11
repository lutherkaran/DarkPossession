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
                if (!gEnemy.bEnemyIsPossessed)
                {

                    if (BulletManager.instance.bullet.go != null)
                    {
                        if (BulletManager.instance.bullet.BulletHit())
                        {
                            Debug.Log("P L A Y E R   D I E D");
                        }
                        else
                        {
                            Debug.Log("P L A Y E R   A L I V E");
                        }
                    }
                }
                else
                {
                    if (BulletManager.instance.bullet.go != null)
                    {
                        if (BulletManager.instance.bullet.BulletHit())
                        {
                            Debug.Log("P L A Y E R   D I E D");
                        }
                        else
                        {
                            Debug.Log("P L A Y E R   A L I V E");
                        }
                    }
                    Debug.Log("ENEMY IS POSSESSED");
                }
            }
            else { Debug.Log("E N E M Y   D I E D"); }
        }
        else { Debug.Log("Enemy Script is disabled"); }

    }
}

