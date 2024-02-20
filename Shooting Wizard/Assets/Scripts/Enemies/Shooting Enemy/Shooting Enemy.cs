using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootingEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public GameObject bullet;

    IDamagable damagable;
    public const float power = 1;
    public const float fieldOfVisionNumber = 7;
    public const float shootingRange = 4;
    public const float reloadingTime = 3;
    public const float bulletForce = 10;
    
    float speed = 3;
    float health = 3;
    GameObject player;

    public float DamageTimer = 1f;
    public float ShootTimer = reloadingTime;
    public float distance;

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
            if (IsShootable())
            {
                if (ShootTimer <= 0)
                {
                    Shoot();
                    ShootTimer = reloadingTime;
                }
                
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        DamageTimer -= Time.deltaTime;
        ShootTimer -= Time.deltaTime;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("Enemy has collided with the player");
            damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if (DamageTimer <= 0)
                {
                    damagable.Damage(power);
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

    private void Shoot()
    {
        Vector2 place = new Vector2(transform.position.x, transform.position.y);
        place.x = place.x - player.transform.position.x;
        place.y = place.y - player.transform.position.y;
        float angle = Mathf.Atan2(place.y, place.x) * Mathf.Rad2Deg + 180f;


        GameObject Shot = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
    }

    private bool IsShootable()
    {
        if (distance < shootingRange)
        {
            return true;
        }
        return false;
    }

    public void MoveTowardsPlayer()
    {
        Vector2 PlayerDirection = player.transform.position - transform.position;
        rb.velocity = PlayerDirection.normalized * speed;

        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public bool PlayerOnSight()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
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
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
