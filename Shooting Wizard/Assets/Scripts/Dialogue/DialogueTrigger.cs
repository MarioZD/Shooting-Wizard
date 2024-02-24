using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public Message[] messages; 

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages);
    }

}

[System.Serializable]
public class Message
{
    public string message;
    public string name;
}