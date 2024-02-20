using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class GunReloadingState : GunBaseState
{
    GunStateManager Gun;
    public override void EnterState(GunStateManager gun)
    {
        Gun = gun;
        Debug.Log("Reloading");
        gun.StartCoroutine(Reloading());
    }
    public override void UpdateState(GunStateManager gun)
    {
        RotateGun();
    }

    void RotateGun()
    {

        Gun.angle = Gun.Player.GetAngle();
        if (Gun.angle < 90 | Gun.angle > 270)
        {
            Gun.gameObject.transform.parent = Gun.firepointLeft;
            Gun.gameObject.transform.position = Gun.firepointLeft.position;
            Gun.transform.rotation = Quaternion.Euler(0, 0, Gun.angle);
        }
        else
        {
            Gun.gameObject.transform.parent = Gun.firepointRight;
            Gun.gameObject.transform.position = Gun.firepointRight.position;
            Gun.transform.rotation = Quaternion.Euler(180, 0, -Gun.angle);

        }
    }

    public IEnumerator Reloading()
    {
        yield return new WaitForSeconds(Gun.reloadAmount);
        Gun.ammo = Gun.MaxAmmo;
        Gun.SwitchState(Gun.activeState);
    }
}

