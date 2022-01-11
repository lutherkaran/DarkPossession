using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool bIsPlayerCollided = false;
    public Enemy enemy;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        if (enemy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                bIsPlayerCollided = true;
                EnemyManager.instance.gEnemy.Attack();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bIsPlayerCollided = false;
        }
    }
}
