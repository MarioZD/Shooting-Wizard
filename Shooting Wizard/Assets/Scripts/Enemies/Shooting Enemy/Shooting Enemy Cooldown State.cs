using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShootingEnemyCooldownState : EnemyBaseState
{
    IDamagable damagable;
    ShootingEnemyStateManager CurrentEnemy;
    float angle;
    Rigidbody2D rb;
    GameObject player;

    float cooldown;
    public override void EnterState(ShootingEnemyStateManager enemy)
    {
        enemy.rb.velocity = Vector3.zero;
        CurrentEnemy = enemy;
        cooldown = CurrentEnemy.cooldownTime;
        player = enemy.player;
        rb = enemy.rb;

    }
    public override void UpdateState(ShootingEnemyStateManager enemy)
    {

        Vector2 PlayerDirection = player.transform.position - enemy.transform.position;
        angle = Mathf.Atan2(PlayerDirection.y, PlayerDirection.x) * Mathf.Rad2Deg + 170f;
        enemy.animator.SetFloat("Angle", angle);

        if (enemy.rb.velocity != new Vector2(0, 0))
        {
            enemy.animator.SetFloat("Movement", 1f);
        }
        else
        {
            enemy.animator.SetFloat("Movement", 0f);
        }


        if (cooldown <= 0)
        {
            enemy.SwitchState(enemy.idleState);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
    public override void OnCollisionStay2D(ShootingEnemyStateManager enemy, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("Enemy has collided with the player");
            damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if ( CurrentEnemy.DamageTimer <= 0)
                {
                    damagable.Damage(CurrentEnemy.physicalPower);
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
}
