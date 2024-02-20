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
    // Start is called before the first frame update


    private void OnEnable()
    {
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunStateManager>();
        
    }

    private void Awake()
    {
        GameManager.FirstBattle += Enable;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.FirstBattle -= Enable;
    }

    void Start()
    {

    }

    // Update is called once per frame
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

    void Enable()
    {
        gameObject.SetActive(true);
    }




}
