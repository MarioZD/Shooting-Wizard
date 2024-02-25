using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public DialogueTrigger StartingDialogue;

    public static event Action FirstBattle;
    public static event Action FirstBattleOver;

    public static event Action SecondBattle;
    public static event Action SecondBattleOver;

    public static event Action ThirdBattle;
    public static event Action ThirdBattleOver;

    public static event Action GameOver;

    public static float enemyCount = 0;
    public static bool onBattle;

    public AudioSource BattleMusic;
    public AudioSource BetweenBattleMusic;


    public void SwitchState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.gunNotPickedUp:
                GunNotPickedUp();
                break;

            case GameState.firstBatlle:
                FirstBattle?.Invoke();
                InFirstBattle();
                onBattle = true;
                break;

            case GameState.betweenBattles:
                break;

            case GameState.secondBattle:
                SecondBattle?.Invoke();
                onBattle = true;
                InSecondBattle();
                break;

            case GameState.thirdBattle:
                ThirdBattle?.Invoke();
                InThirdBattle();
                onBattle = true;
                break;


            case GameState.gameOver:
                GameOver?.Invoke();
                GameIsOver();
                break;
        }

    }


    public enum GameState
    {
        gunNotPickedUp,
        firstBatlle,
        betweenBattles,
        secondBattle,
        thirdBattle,
        gameOver

    }
    // Start is called before the first frame update


    void Awake()
    {
        Instance = this;
        FirstBattle = null;
        FirstBattleOver = null;
        SecondBattle = null;
        SecondBattleOver = null;
        ThirdBattle = null;
        ThirdBattleOver = null;
        GameOver = null;

        StartingDialogue = GetComponent<DialogueTrigger>();

        FirstBattleOver += testingEvent;

        SwitchState(GameState.gunNotPickedUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (onBattle)
        {
            if (enemyCount == 0) 
            { 
                onBattle = false;
                AfterEachBattle(State);
            }
        }
    }


    public void GunNotPickedUp()
    {
        StartingDialogue.StartDialogue();
    }

    public void InFirstBattle()
    {
        BattleMusic.Play();
    }

    public void InSecondBattle()
    {
        BetweenBattleMusic.Stop();
        BattleMusic.Play();
    }

    public void InThirdBattle()
    {
        BetweenBattleMusic.Stop();
        BattleMusic.Play();
    }


    public void GameIsOver()
    {

    }

    public void testingEvent()
    {
        Debug.Log("Battle is over");
    }

    public void AfterEachBattle(GameState state)
    {
        switch (state) 
        {
            case GameState.firstBatlle:
                StartCoroutine(StopAndStartMusic(BattleMusic, BetweenBattleMusic));
                FirstBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;

            case GameState.secondBattle:
                StartCoroutine(StopAndStartMusic(BattleMusic, BetweenBattleMusic));
                SecondBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;

            case GameState.thirdBattle:
                StartCoroutine(StopAndStartMusic(BattleMusic, BetweenBattleMusic));
                ThirdBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;


        }

    }

    public IEnumerator StopAndStartMusic(AudioSource lastSong, AudioSource nextSong)
    {
        nextSong.volume = 0;
        float transitionTime = 1f;
        float defaultVolume = lastSong.volume;
        float percentage = 0;
        while (lastSong.volume > 0)
        {
            lastSong.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;

        }
        lastSong.Stop();
        lastSong.volume = defaultVolume;
        nextSong.Play();
        percentage = 0;
        while (lastSong.volume < 100)
        {
            nextSong.volume = Mathf.Lerp(0, defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }


    }

    public void FinishBattle()
    {
        onBattle = false;
    }

    public void EnemyDead()
    {
        enemyCount--;
    }

}
