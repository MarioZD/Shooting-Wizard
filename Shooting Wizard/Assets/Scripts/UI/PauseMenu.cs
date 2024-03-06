using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    // public bool paused;
    public GameObject PauseMenuUI;

    private void Awake()
    {
        PauseMenuUI.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // paused = GameIsPaused;
        if (!DialogueManager.isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                if (GameIsPaused) { 
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        }
    }


    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartLevel()
    {
        Resume();
        GameManager.Instance.FinishBattle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Options()
    {

    }
    
    public void BackToMenu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void ChangeVolume(float volume)
    {

    }
}
