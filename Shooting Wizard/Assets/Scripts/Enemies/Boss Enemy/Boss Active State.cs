using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActiveState : BossBaseState
{
    public GameObject player;
    BossStateManager CurrentEnemy;
    IDamagable damagable;
    float PhysicalPower;
    float distance;
    float cooldown;
    float consecutiveCircler = 0;


    public override void EnterState(BossStateManager enemy)
    {
        player = enemy.player;
        CurrentEnemy = enemy;
        PhysicalPower = enemy.physicalPower;
        cooldown = enemy.cooldownTime;
        enemy.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public override void UpdateState(BossStateManager enemy)
    {
        
        if (cooldown <= 0)
        {

            cooldown = enemy.cooldownTime;
            if (consecutiveCircler >= 3)
            {
                consecutiveCircler = 0;
                enemy.SwitchState(enemy.consecutiveShootingState); 

            }
            else
            {
                consecutiveCircler++;
                enemy.SwitchState(enemy.circleShootingState);
            }
        }
        else
        {
            MoveTowardsPlayer();
            cooldown -= Time.deltaTime;
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



    public void MoveTowardsPlayer()
    {
        Vector2 PlayerDirection = player.transform.position - CurrentEnemy.transform.position;
        CurrentEnemy.rb.velocity = PlayerDirection.normalized * CurrentEnemy.speed;

        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    
}
