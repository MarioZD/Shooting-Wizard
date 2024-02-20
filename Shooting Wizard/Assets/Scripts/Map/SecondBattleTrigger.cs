using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SwitchState(GameManager.GameState.secondBattle);
            Destroy(gameObject);
        }
    }
}
