using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    public GameObject player;
    PlayerStateManager stateManager;
    public Sprite normalSprite;
    public Sprite SelectedSprite;
    public SpriteRenderer SpriteRenderer;

    public GameObject interactableKey;

    IDamagable playerDamagable;
    float interactabilityDistance = 2.3f;
    public float healAmount = 2;
    bool interactable = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stateManager = player.GetComponent<PlayerStateManager>();
        playerDamagable = player.GetComponent<IDamagable>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractability();
        if (interactable)
        {
            if (Input.GetKeyDown("e"))
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
        playerDamagable.Health += healAmount;
        if (playerDamagable.Health > stateManager.maxHealth) 
        {
            playerDamagable.Health = stateManager.maxHealth;
        }

        Destroy(gameObject);
    }

    public void GetInteractability()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distance < interactabilityDistance)
        {
            interactable = true;
            interactableKey.SetActive(true);
            SpriteRenderer.sprite = SelectedSprite;

        }

        else
        {
            interactable = false;
            interactableKey.SetActive(false);
            SpriteRenderer.sprite = normalSprite;
        }
    }
}
