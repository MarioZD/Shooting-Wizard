using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingEnemyDeadState : EnemyBaseState
{
    public override void EnterState(ShootingEnemyStateManager enemy)
    {
        UnityEngine.GameObject.Destroy(enemy.gameObject);
    }
    public override void UpdateState(ShootingEnemyStateManager enemy)
    {

    }
    public override void OnCollisionStay2D(ShootingEnemyStateManager enemy, Collision2D collision)
    {

    }
}
