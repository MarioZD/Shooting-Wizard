using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BossConsecutiveShootingState : BossBaseState
{

    BossStateManager CurrentEnemy;
    float PhysicalPower;
    IDamagable damagable;
    GameObject player;
    const float betweenShotsTime = 0.3f;
    float timer; 
    float currentBetweenShotsTime;

    public override void EnterState(BossStateManager enemy)
    {
        CurrentEnemy = enemy;
        PhysicalPower = enemy.physicalPower;
        player = enemy.player;
        timer = enemy.consecutiveShootingTimer;
        enemy.rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }
    public override void UpdateState(BossStateManager enemy)
    {
        if(!(timer <= 0))
        {
            if (currentBetweenShotsTime <= 0)
            {
                ConsecutiveShoot();
                currentBetweenShotsTime = betweenShotsTime;
            }
            else
            {
                currentBetweenShotsTime -= Time.deltaTime;
            }

            timer -= Time.deltaTime;

        }
        else
        {
            enemy.SwitchState(enemy.activeState);
        }
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

    void ConsecutiveShoot()
    {
        if (CurrentEnemy == null)
        {

        }
        else
        {
            Vector2 place = new Vector2(CurrentEnemy.transform.position.x, CurrentEnemy.transform.position.y);
            place.x = place.x - player.transform.position.x;
            place.y = place.y - player.transform.position.y;
            float angle = Mathf.Atan2(place.y, place.x) * Mathf.Rad2Deg + 180f;
            UnityEngine.GameObject.Instantiate(CurrentEnemy.consecutiveBullet, CurrentEnemy.firepoint.position, Quaternion.Euler(0, 0, angle));
        }
    }
}


