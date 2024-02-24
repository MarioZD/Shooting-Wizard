using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateManager : MonoBehaviour
{
    public GunBaseState activeState = new GunActiveState();
    public GunBaseState reloadingState = new GunReloadingState();


    public GunBaseState currentState;

    public float MaxAmmo = 15;
    public float cooldownAmount = 0.3f;
    public float reloadAmount = 3f;
    public float cooldownTime = 0;
    public float reloadTime = 3f;
    public float ammo = 15;
    public float bulletForce = 10f;
    public PlayerStateManager Player;
    public Transform shootPoint;

    public Vector3 mousePosition;
    public Vector3 objectPosition;
    public float angle;

    public GameObject bullet;
    public Transform firepointLeft;
    public Transform firepointRight;
    public Texture2D cursor;
    public AudioSource ShootSound;

    private void Awake()
    {
        firepointLeft = GameObject.Find("Firepoint_left").transform;
        firepointRight = GameObject.Find("Firepoint_right").transform;
        shootPoint = transform.GetComponentInChildren<Transform>();
        Player = GameObject.Find("Player").GetComponent<PlayerStateManager>();
    }


    void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
        currentState = activeState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused && !DialogueManager.isActive)
        {
            currentState.UpdateState(this);
        }
    }

    public void SwitchState(GunBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
