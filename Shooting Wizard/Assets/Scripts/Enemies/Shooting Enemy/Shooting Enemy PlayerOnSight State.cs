using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ShootingEnemyPlayerOnSightState : EnemyBaseState
{
    public GameObject player;
    ShootingEnemyStateManager CurrentEnemy;
    IDamagable damagable;
    float PhysicalPower;
    float distance;
    float firstCooldown;
    float angle;
    Rigidbody2D rb;
    Vector2 PlayerDirection;


    public override void EnterState(ShootingEnemyStateManager enemy)
    {
        player = enemy.player;
        CurrentEnemy = enemy;
        PhysicalPower = enemy.physicalPower;
        firstCooldown = enemy.cooldownTime;
        rb = enemy.rb;
        enemy.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public override void UpdateState(ShootingEnemyStateManager enemy)
    {
        PlayerOnSight();

        PlayerDirection = player.transform.position - CurrentEnemy.transform.position;
        angle = Mathf.Atan2(PlayerDirection.y, PlayerDirection.x) * Mathf.Rad2Deg + 170f;
        CurrentEnemy.animator.SetFloat("Angle", angle);

        

        if (IsShootable())
        {
            
            Shoot();
            enemy.SwitchState(enemy.cooldownState);
        }
        else
        {
            MoveTowardsPlayer();
        }

        if (rb.velocity != new Vector2(0, 0))
        {
           enemy.animator.SetFloat("Movement", 1f);
        }
        else
        {
            enemy.animator.SetFloat("Movement", 0f);
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


    public void PlayerOnSight()
    {
        distance = Vector3.Distance(CurrentEnemy.transform.position, player.transform.position);
        if (distance < CurrentEnemy.fieldOfVisionNumber)
        {
            // Debug.Log("Player is on sight of the enemy, distance is " + distance);
        }
        else
        {
            // Debug.Log("Player isnt on sight of the enemy, distance is " + distance);
            CurrentEnemy.SwitchState(CurrentEnemy.idleState);

        }
    }

    public void MoveTowardsPlayer()
    {
        CurrentEnemy.rb.velocity = PlayerDirection.normalized * CurrentEnemy.speed;


        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private bool IsShootable()
    {
        if (distance < CurrentEnemy.shootingRange)
        {
            return true;
        }
        return false;
    }

    private void Shoot()
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
            UnityEngine.GameObject.Instantiate(CurrentEnemy.bullet, CurrentEnemy.firepoint.position , Quaternion.Euler(0, 0, angle));
        }


    }

}
