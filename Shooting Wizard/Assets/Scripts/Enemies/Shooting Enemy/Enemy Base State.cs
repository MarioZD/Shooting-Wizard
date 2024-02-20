using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(ShootingEnemyStateManager enemy);
    public abstract void UpdateState(ShootingEnemyStateManager enemy);
    public abstract void OnCollisionStay2D(ShootingEnemyStateManager enemy, Collision2D collision);


}
