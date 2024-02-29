using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.GameOver += EnableScreen;

    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= EnableScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableScreen()
    {
        gameObject.SetActive(true);
        GameManager.Instance.FinishBattle(); 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
