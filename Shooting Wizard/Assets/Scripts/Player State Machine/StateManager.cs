using JetBrains.Annotations;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour, IDamagable

 {
    public PlayerBaseState currentState;
    public PlayerActiveState activeState = new PlayerActiveState();
    public Animator animator;
    public PlayerDeadState deadState = new PlayerDeadState();

    private Vector3 mousePosition;
    private Vector3 objectPosition;
    public float angle;

    public Rigidbody2D rb;

    public float maxHealth = 6;
    public float speed = 6;

    void Start()
    {
        currentState = activeState;
        currentState.EnterState(this);
    }

    void Update()
    {
        
        if (!DialogueManager.isActive)
        {
            if (currentState != deadState)
            {
                angle = GetAngle();
                animator.SetFloat("Angle", angle);
            }

            currentState.UpdateState(this);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetFloat("Movement", 0f);
        }

        if (Health <= 0 )
        {
            SwitchState(deadState); 
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public float GetAngle()
    {
        
        mousePosition = Input.mousePosition;
        objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPosition.x;
        mousePosition.y -= objectPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x)* Mathf.Rad2Deg + 170f;
        return angle;
}
    
    public void Damage(float DamageAmont)
    {
        activeState.Damage(DamageAmont);
        animator.SetTrigger("Hit");
    }

    public float Health
    {
        get { return activeState.Health; }
        set { activeState.Health = value; }

    }

    public void PlayerAway(Vector2 towhere)
    {
        rb.velocity = towhere;
    }



}
