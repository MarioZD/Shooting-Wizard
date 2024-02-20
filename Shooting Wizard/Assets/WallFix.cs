using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFix : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionPosition = collision.transform.position; 
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 push;
            push.x = collision.gameObject.transform.position.x - collisionPosition.x;
            push.y = collision.gameObject.transform.position.y - collisionPosition.y;
            collisionPosition *= 2f;
            Debug.Log("Player has collided with wall");
            collision.gameObject.GetComponent<PlayerStateManager>().PlayerAway(collisionPosition);
          
        }
    }
}
