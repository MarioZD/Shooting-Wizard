using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsThatDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger dialogue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.StartDialogue();
            Destroy(gameObject);
        }
    }
}
