using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclerBullet : MonoBehaviour
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
        rb = GetComponent<Rigidbody2D>();
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
