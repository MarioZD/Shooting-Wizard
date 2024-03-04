using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCircleShootingState : BossBaseState
{
    BossStateManager CurrentEnemy;
    float PhysicalPower;
    IDamagable damagable;
    GameObject player;

    // Start is called before the first frame update
    public override void EnterState(BossStateManager enemy)
    {
        CurrentEnemy = enemy;
        PhysicalPower = enemy.physicalPower;
        player = enemy.player;
    }
    public override void UpdateState(BossStateManager enemy)
    {
        CircleShoot();
        enemy.SwitchState(enemy.activeState);
    }
    public override void OnCollisionStay2D(BossStateManager enemy, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if (CurrentEnemy.DamageTimer <= 0)
                {
                    damagable.Damage(PhysicalPower);
                    // Debug.Log("Player health is " + damagable.Health);
                    CurrentEnemy.DamageTimer = CurrentEnemy.DamageTimerDefault;

                }

            }
            else
            {
                Debug.Log("No Idamagable in player");
            }
        }
    }


    private void CircleShoot()
    {

        if (CurrentEnemy == null)
        {

        }
        else
        {
            for (int i = 0; i < CurrentEnemy.bulletAmount; i++)
            {
                float angle = (360 / CurrentEnemy.bulletAmount) * (i + 1);
                GameObject Shoot = UnityEngine.GameObject.Instantiate(CurrentEnemy.circleBullet, CurrentEnemy.firepoint.position, Quaternion.Euler(0, 0, angle));

            }


        }

    }
}
