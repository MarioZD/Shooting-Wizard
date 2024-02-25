using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTrigger : MonoBehaviour
{
    void Start()
    {
        GetComponent<DialogueTrigger>().StartDialogue();
        StartWaiting();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartWaiting()
    {
        StartCoroutine(WaitUntilDialogueFinished());
    }
    IEnumerator WaitUntilDialogueFinished()
    {
        while (DialogueManager.isActive)
        {
            yield return null;
        }

        SceneManager.LoadScene("First Scene");
    }

}
