using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GunActiveState : GunBaseState
{
    GunStateManager Gun;
    CinemachineImpulseSource GunShake;
    public override void EnterState(GunStateManager gun)
    {
        Gun = gun;
        GunShake = gun.GetComponent<CinemachineImpulseSource>();
    }
    public override void UpdateState(GunStateManager gun)
    {
        RotateGun();
        if (gun.ammo <= 0 | Input.GetKeyDown("r"))
        {
            gun.SwitchState(gun.reloadingState);
        }
        if (gun.cooldownTime <= 0) {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                gun.cooldownTime = gun.cooldownAmount;
            }
        }
        else
        {
            gun.cooldownTime -= Time.deltaTime;
        }    
    }

    void Shoot()
    {
        Gun.ShootSound.Play();
        CameraShakeManager.Instance.CameraShake(GunShake, 0.1f);
        UnityEngine.GameObject.Instantiate(Gun.bullet, Gun.shootPoint.position, Quaternion.Euler(0, 0, Gun.angle));
        Gun.ammo -= 1;
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


}
