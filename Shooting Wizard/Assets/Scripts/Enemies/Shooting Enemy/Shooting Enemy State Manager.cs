using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyStateManager : MonoBehaviour, IDamagable
{
    public EnemyBaseState idleState = new ShootingEnemyIdleState();
    public EnemyBaseState onSightState = new ShootingEnemyPlayerOnSightState();
    public EnemyBaseState cooldownState = new ShootingEnemyCooldownState();
    public EnemyBaseState deadState = new ShootingEnemyDeadState();

    public GameObject player;
    public GameObject bullet;
    public Transform firepoint;
    public Rigidbody2D rb;
    public GameObject[] drops;
    public Animator animator;

    EnemyBaseState currentState;
    public float shootingRange = 12f;
    public float speed = 3f;
    public float health = 4;
    public float DamageTimerDefault = 1f;
    public float DamageTimer = 1f;
    public float physicalPower = 1f;
    public float fieldOfVisionNumber = 15f;
    public float cooldownTime = 2f;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        GameManager.Instance.enemyCount++;
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentState = cooldownState;
        currentState.EnterState(this);
    }


    void Update()
    {
        if (!DialogueManager.isActive)
        {
            currentState.UpdateState(this);
            DamageTimer -= Time.deltaTime;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        currentState.OnCollisionStay2D(this, collision);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }


    public void Damage(float damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            SwitchState(deadState);
        }
        animator.SetTrigger("Hit");
    }

    private void OnDestroy()
    {
        GameManager.Instance.EnemyDead();
    }

}
