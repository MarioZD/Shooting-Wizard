using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class AmmoManager : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_Text textAmmo;
    public GunStateManager gun;



    private void OnEnable()
    {
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunStateManager>();
        
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }


    void Update()
    {
        if (gun.currentState == gun.reloadingState)
        {
            textAmmo.text = "Reloading";
        }
        else
        {
            textAmmo.text = gun.ammo.ToString() + " / " + gun.MaxAmmo;

        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }




}
