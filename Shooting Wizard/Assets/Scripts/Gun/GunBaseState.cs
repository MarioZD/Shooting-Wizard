using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBaseState
{
    public abstract void EnterState(GunStateManager gun);
    public abstract void UpdateState(GunStateManager gun);
}
