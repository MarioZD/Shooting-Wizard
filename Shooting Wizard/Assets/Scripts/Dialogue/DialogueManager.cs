using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text message;
    public RectTransform messageBox;

    Message[] currentMessages;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages)
    {
        activeMessage = 0;
        isActive = true;
        messageBox.localScale = Vector3.one;
        currentMessages = messages;
        DisplayMessage();
    }

    private void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        message.text = messageToDisplay.message;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            messageBox.localScale = Vector3.zero;
            isActive = false;
        }
        
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetButtonDown("Fire1") | Input.GetKeyDown(KeyCode.Space))
            {
                NextMessage();
            }
        }
    }
}
