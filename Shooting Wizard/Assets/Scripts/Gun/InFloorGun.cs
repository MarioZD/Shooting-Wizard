using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFloorGun : MonoBehaviour, IInteractable
{
    public bool interactable;
    public GameObject player;
    public GameObject gun_object;
    public Transform firepoint;
    public GameManager gameManager;
    public GameObject interactableKey;

    public float interactabilityDistance = 2.3f;

    public Sprite normalSprite;
    public Sprite SelectedSprite;
    public SpriteRenderer SpriteRenderer;

    private void Start()
    {
        gameManager = GameManager.Instance;
        interactableKey.SetActive(false);
    }
    private void Update()
    {
        

        GetInteractability();
        if (Input.GetKeyDown("e"))
        {
            if (interactable)
            {
                Interact();
            }
        }
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

    public void Interact()
    {
        Instantiate(gun_object, firepoint.transform);

        Destroy(gameObject);
        Debug.Log("You interacted with this object");

        gameManager.SwitchState(GameManager.GameState.firstBatlle);
    }
}
