using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingEnemyDeadState : EnemyBaseState
{
    ShootingEnemyStateManager currentEnemy;
    public override void EnterState(ShootingEnemyStateManager enemy)
    {
        currentEnemy = enemy;
        Drop(enemy.drops);
        UnityEngine.GameObject.Destroy(enemy.gameObject);
    }
    public override void UpdateState(ShootingEnemyStateManager enemy)
    {

    }
    public override void OnCollisionStay2D(ShootingEnemyStateManager enemy, Collision2D collision)
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
