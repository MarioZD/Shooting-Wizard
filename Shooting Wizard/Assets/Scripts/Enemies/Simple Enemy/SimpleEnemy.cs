using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] public Rigidbody2D rb;
    
    IDamagable damagable;
    const float power = 1;
    const float fieldOfVisionNumber = 7;
    float attackDistance = 3;
    float speed = 3;
    float health = 3;
    GameObject player;

    public float DamageTimer = 1f;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        
       if (PlayerOnSight())
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            MoveTowardsPlayer();
        }
       else
        {
           rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        DamageTimer -= Time.deltaTime; 
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("Enemy has collided with the player");
            damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null )
            {
                if (DamageTimer <= 0)
                {
                    damagable.Damage(power);
                    Debug.Log("Player health is " + damagable.Health);
                    DamageTimer = 1f;

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
        Vector2 PlayerDirection = player.transform.position - transform.position;
        rb.velocity = PlayerDirection.normalized * speed;

        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public bool PlayerOnSight()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < fieldOfVisionNumber)
        {
            // Debug.Log("Player is on sight of the enemy, distance is " + distance);
            return true;
        }
        else
        {
            // Debug.Log("Player isnt on sight of the enemy, distance is " + distance);
            return false;
        }
    }


    public void Damage(float damageAmount)
    {
        Health -= damageAmount;
        if (health <= 0 )
        {
            Die();
        }    
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.EnemyDead();
    }
}
