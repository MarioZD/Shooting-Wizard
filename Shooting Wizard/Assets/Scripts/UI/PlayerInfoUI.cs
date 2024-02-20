using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        
        GameManager.FirstBattle += EnablePlayerInfo;
        DisablePlayerInfo();
    }

    void OnDestroy()
    {
        GameManager.FirstBattle -= EnablePlayerInfo;
        DisablePlayerInfo();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisablePlayerInfo()
    {
        gameObject.SetActive(false);
    }


    void EnablePlayerInfo()
    {
        gameObject.SetActive(true);
    }
}
