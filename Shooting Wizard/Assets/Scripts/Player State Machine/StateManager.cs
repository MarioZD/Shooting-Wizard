using JetBrains.Annotations;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour, IDamagable

 {
    public PlayerBaseState currentState;
    public PlayerActiveState activeState = new PlayerActiveState();
    public PlayerDeadState deadState = new PlayerDeadState();

    private Vector3 mousePosition;
    private Vector3 objectPosition;
    public float angle;

    public Rigidbody2D rb;

    public float maxHealth = 6;

    void Start()
    {
        currentState = activeState;
        currentState.EnterState(this);
    }

    void Update()
    {
        if (!DialogueManager.isActive)
        {
            currentState.UpdateState(this);
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
