using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CirclerEnemyBaseState
{
    public abstract void EnterState(CirclerEnemyStateManager enemy);
    public abstract void UpdateState(CirclerEnemyStateManager enemy);
    public abstract void OnCollisionStay2D(CirclerEnemyStateManager enemy, Collision2D collision);


}
