using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BossStateManager : MonoBehaviour, IDamagable
{
    BossBaseState deadState = new BossDeadState();

    public GameObject player;
    public GameObject bullet;
    public Transform firepoint;
    public Rigidbody2D rb;
    public GameObject[] drops;
    public Animator animator;

    BossBaseState currentState;
    public float shootingRange = 6f;
    public float speed = 3f;
    public float health = 4f;
    public float DamageTimerDefault = 1f;
    public float DamageTimer = 1f;
    public float physicalPower = 1f;
    public float fieldOfVisionNumber = 9f;
    public float cooldownTime = 2f;
    public float bulletAmount = 8;

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
        currentState = deadState;
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

    public void SwitchState(BossBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
        Debug.Log("Circler enemy is on " + currentState + "state");
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
