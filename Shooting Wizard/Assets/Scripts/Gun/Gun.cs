
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public float MaxAmmo = 15;
    private const float cooldownAmount = 0.5f;
    private const float reloadAmount = 3f;
    private float cooldownTime = 0;
    private float reloadTime = 3f;
    public float ammo = 15;
    public float bulletForce = 10f;
    public PlayerStateManager Player;
    public Transform shootPoint;


    private Vector3 mousePosition;
    private Vector3 objectPosition;
    public float angle;



    public GameObject bullet;
    public Transform firepointLeft;
    public Transform firepointRight;



    // Start is called before the first frame update
    void Start()
    {
        firepointLeft = GameObject.Find("Firepoint_left").transform;
        firepointRight = GameObject.Find("Firepoint_right").transform;
        Player = GameObject.Find("Player").GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();

        if (!IsOnCooldown() && !OnReload()) 
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                ammo -= 1;
                if (ammo <= 0)
                {
                    Reload();
                }
                else
                {
                    SetCooldown();
                }

            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shootPoint.position, Quaternion.Euler(0,0,angle));
        /*GameObject Shot = Instantiate(bullet, shootPoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D bulletRb = Shot.GetComponent<Rigidbody2D>();
        bulletRb.AddForce((-this.transform.right) * bulletForce, ForceMode2D.Impulse);*/
    }

    void SetCooldown()
    {
        cooldownTime = cooldownAmount;

    }
    bool IsOnCooldown()
    {
        if (cooldownTime <= 0 )
        {
            return false;
        }
        else
        {
            cooldownTime -= Time.deltaTime;
            return true;
        }
    }

    void Reload()
    {
        reloadTime = reloadAmount;
    }
    
    bool OnReload()
    {
        if (reloadTime < 1) 
        {
            ammo = MaxAmmo;
        }
        if (reloadTime <= 0)
        {
            return false;
        }
        else
        {
            reloadTime -= Time.deltaTime;
            return true;
        }

        
    }

    void RotateGun()
    {
        
        angle = Player.GetAngle();
        if (angle < 90 | angle > 270)
        {
            gameObject.transform.parent = firepointLeft;
            gameObject.transform.position = firepointLeft.position;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else 
        {
            gameObject.transform.parent = firepointRight;
            gameObject.transform.position = firepointRight.position;
            transform.rotation = Quaternion.Euler(180, 0, -angle);
            
        }

        
    }





}
