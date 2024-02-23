using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CirclerEnemyIdleState : CirclerEnemyBaseState
{
    public GameObject player;
    CirclerEnemyStateManager CurrentEnemy;
    IDamagable damagable;
    float DamageTimer;
    float distance;

    
    public override void EnterState(CirclerEnemyStateManager enemy)
    {
        CurrentEnemy = enemy;
        player = enemy.player;
        DamageTimer = enemy.DamageTimer;
        enemy.rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public override void UpdateState(CirclerEnemyStateManager enemy)
    {
        PlayerOnSight();
        DamageTimer -= Time.deltaTime;
    }
    public override void OnCollisionStay2D(CirclerEnemyStateManager enemy, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if (DamageTimer <= 0)
                {
                    damagable.Damage(CurrentEnemy.physicalPower);
                    // Debug.Log("Player health is " + damagable.Health);
                    DamageTimer = 1f;

                }

            }
            else
            {
                Debug.Log("No Idamagable in player");
            }
        }
    }


    public void PlayerOnSight()
    {
        distance = Vector3.Distance(CurrentEnemy.transform.position, player.transform.position);
        if (distance < CurrentEnemy.fieldOfVisionNumber)
        {
            // Debug.Log("Player is on sight of the enemy, distance is " + distance);
            CurrentEnemy.SwitchState(CurrentEnemy.onSightState);
        }
        else
        {
            // Debug.Log("Player isnt on sight of the enemy, distance is " + distance);
            
        }
    }




}
