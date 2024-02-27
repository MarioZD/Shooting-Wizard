using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActiveState : PlayerBaseState, IDamagable
{
    float health = 6;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    Vector2 movement;
    float speed;
    Rigidbody2D rb;
    Camera cam;
    Vector2 mousePos;

    public override void EnterState(PlayerStateManager player)
    {
        rb = player.rb;
        speed = player.speed;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        PlayerMove();
    }
    public void Damage(float DamageAmount)
    {
        Debug.Log("Player has been damaged, life remaining " + health);
        Health -= DamageAmount;
        
    }
    public override void OnCollisionEnter(PlayerStateManager player)
    {
     
    }


    public void PlayerMove()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized * speed;
        rb.velocity = movement;

    }

}
