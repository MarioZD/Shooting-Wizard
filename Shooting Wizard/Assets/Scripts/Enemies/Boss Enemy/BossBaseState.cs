using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(BossStateManager enemy);
    public abstract void UpdateState(BossStateManager enemy);
    public abstract void OnCollisionStay2D(BossStateManager enemy, Collision2D collision);


}