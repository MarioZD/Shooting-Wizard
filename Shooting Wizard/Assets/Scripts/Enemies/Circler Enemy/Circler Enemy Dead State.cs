using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CirclerEnemyDeadState : CirclerEnemyBaseState
{
    public override void EnterState(CirclerEnemyStateManager enemy)
    {
        UnityEngine.GameObject.Destroy(enemy.gameObject);
    }
    public override void UpdateState(CirclerEnemyStateManager enemy)
    {

    }
    public override void OnCollisionStay2D(CirclerEnemyStateManager enemy, Collision2D collision)
    {

    }
}
