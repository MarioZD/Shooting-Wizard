using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorVerticalScript : MonoBehaviour
{
    public GameObject upDoor;
    public GameObject downDoor;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.SecondBattle += CloseDoor;
        GameManager.SecondBattleOver += OpenDoor;
    }

    private void OnDestroy()
    {
        GameManager.SecondBattleOver -= OpenDoor;
        GameManager.SecondBattle -= CloseDoor;
    }

    public void OpenDoor()
    {
        upDoor.transform.rotation = Quaternion.Euler(0, 0, 270f);
        downDoor.transform.rotation = Quaternion.Euler(0, 0, 90f);

        
    }

    public void CloseDoor()
    {
        upDoor.transform.rotation = Quaternion.Euler(0, 0, 0);
        downDoor.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
