using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        
        DisablePlayerInfo();
    }

    void OnDestroy()
    {
        DisablePlayerInfo();
    }

    public void DisablePlayerInfo()
    {
        gameObject.SetActive(false);
    }


    public void EnablePlayerInfo()
    {
        gameObject.SetActive(true);
    }
}
