using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyBullet : MonoBehaviour
{
    float lifeTime = 5f;
    float damage_amount = 1f;
    IDamagable damagable;

    GameObject player;
    Rigidbody2D rb;
    private Vector3 playerPosition;
    private Vector3 enemyPosition;
    public float angle;
    public float bulletForce = 6f;


    private void Start()
    {
        player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody2D>();
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        playerPosition.x -= enemyPosition.x;
        playerPosition.y -= enemyPosition.y;
        angle = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg + 180f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.AddForce((-transform.right) * bulletForce, ForceMode2D.Impulse);
    }
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {

            damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(damage_amount);
            }
            Destroy(gameObject);
        }
    }
}
