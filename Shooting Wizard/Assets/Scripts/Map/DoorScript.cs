using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public Sprite leftDoorOpen;
    public Sprite rightDoorOpen;

    public Sprite leftDoorClosed;
    public Sprite rightDoorClosed;


    // Start is called before the first frame update


    public void OpenDoor()
    {
        leftDoor.GetComponent<SpriteRenderer>().sprite = leftDoorOpen;
        rightDoor.GetComponent<SpriteRenderer>().sprite = rightDoorOpen;

        Destroy(leftDoor.GetComponent<PolygonCollider2D>());
        leftDoor.AddComponent<PolygonCollider2D>();
        Destroy(rightDoor.GetComponent<PolygonCollider2D>());
        rightDoor.AddComponent<PolygonCollider2D>();
    }

    public void CloseDoor()
    {
        leftDoor.GetComponent<SpriteRenderer>().sprite = leftDoorClosed;
        rightDoor.GetComponent<SpriteRenderer>().sprite = rightDoorClosed;
        Destroy(leftDoor.GetComponent<PolygonCollider2D>());
        leftDoor.AddComponent<PolygonCollider2D>();
        Destroy(rightDoor.GetComponent<PolygonCollider2D>());
        rightDoor.AddComponent<PolygonCollider2D>();
    }

}
