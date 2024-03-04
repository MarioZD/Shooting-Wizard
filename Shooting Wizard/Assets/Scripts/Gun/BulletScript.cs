using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float lifeTime = 5f;
    float damage_amount = 1f;
    IDamagable damagable;

    private Vector3 mousePosition;
    private Vector3 objectPosition;
    public float angle;
    public float bulletForce = 10f;

    public Rigidbody2D rb;
    

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePosition = Input.mousePosition;
        objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPosition.x;
        mousePosition.y -= objectPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg + 180f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.AddForce((-transform.right) * bulletForce, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
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

