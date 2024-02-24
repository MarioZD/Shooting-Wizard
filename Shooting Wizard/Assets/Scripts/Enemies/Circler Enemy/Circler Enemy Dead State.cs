using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CirclerEnemyDeadState : CirclerEnemyBaseState
{
    CirclerEnemyStateManager currentEnemy;
    public override void EnterState(CirclerEnemyStateManager enemy)
    {
        currentEnemy = enemy;
        Drop(enemy.drops);
        UnityEngine.GameObject.Destroy(enemy.gameObject);
    }
    public override void UpdateState(CirclerEnemyStateManager enemy)
    {

    }
    public override void OnCollisionStay2D(CirclerEnemyStateManager enemy, Collision2D collision)
    {

    }

    void Drop(GameObject[] drops)
    {
        if (drops != null)
        {
            if (Random.Range(1, 10) >= 9)
            {
                UnityEngine.GameObject.Instantiate(drops[Random.Range(0, drops.Length - 1)], currentEnemy.transform.position, Quaternion.identity);
            }

        }
    }
}
