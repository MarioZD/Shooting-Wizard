using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is dead");
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Gun"));
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.SwitchState(GameManager.GameState.gameOver);
        player.animator.SetFloat("Movement", 0f);
    }
    public override void UpdateState(PlayerStateManager player)
    {

    }
    public override void OnCollisionEnter(PlayerStateManager player)
    {

    }

}
