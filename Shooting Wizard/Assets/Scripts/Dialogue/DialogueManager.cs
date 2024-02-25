using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text message;
    public TMP_Text sayer;
    public RectTransform messageBox;
    public float textSpeed = 0.001f;

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
        sayer.text = currentMessages[activeMessage].name;
        StartCoroutine(DisplayingMessage());
    }

    IEnumerator DisplayingMessage()
    {
        message.text = string.Empty;
        foreach (char c in currentMessages[activeMessage].message.ToCharArray()) 
        {
            message.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextMessage()
    {
        if (message.text == currentMessages[activeMessage].message)
        {
            activeMessage++;
            if (activeMessage < currentMessages.Length)
            {
                StopAllCoroutines();
                DisplayMessage();
            }
            else
            {
                StopAllCoroutines();
                messageBox.localScale = Vector3.zero;
                isActive = false;
            }
        }

        else
        {
            StopAllCoroutines();
            message.text = currentMessages[activeMessage].message;
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
