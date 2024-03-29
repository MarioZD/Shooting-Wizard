using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public GameState startingState; 

    public DialogueTrigger StartingDialogue;
    #region First Scene
    public UnityEvent FirstBattle;
    public UnityEvent FirstBattleOver;
            
    public UnityEvent SecondBattle;
    public UnityEvent SecondBattleOver;
            
    public UnityEvent ThirdBattle;
    public UnityEvent ThirdBattleOver;
    #endregion

    #region Second Scene

    public UnityEvent secondRoom;

    public UnityEvent SecondRoomBattle;
    public UnityEvent SecondRoomBattleOver;

    public UnityEvent bossBattle;
    public UnityEvent bossBattleOver;

    #endregion

    public static event Action Win;

    public static event Action GameOver;

    [SerializeField] public float enemyCount = 0;
    [SerializeField] public bool onBattle;

    public AudioSource BattleMusic;
    public AudioSource BetweenBattleMusic;
    public AudioSource FirstBattleMusic;
    public AudioSource Torches;

    public void SwitchState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.gunNotPickedUp:
                Torches.Play();
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

            case GameState.secondRoom:
                secondRoom.Invoke();
                break;

            case GameState.secondRoomBattle:
                SecondRoomBattle?.Invoke();
                InSecondRoomBattle();
                onBattle = true;
                break;

            case GameState.bossBattle:
                BossBattle();
                bossBattle.Invoke();
                onBattle = true;
                break;

            case GameState.winState:
                WinScreen();
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
        secondRoom,
        secondRoomBattle,
        bossBattle,
        winState,
        gameOver

    }
    // Start is called before the first frame update


    void Awake()
    {
        Instance = this;


        StartingDialogue = GetComponent<DialogueTrigger>();


        
    }
    private void Start()
    {
        enemyCount = 0;
        SwitchState(startingState);
    }
    // Update is called once per frame
    void Update()
    {
        if (onBattle)
        {
            if (enemyCount <= 0) 
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
        Torches.Stop();
        FirstBattleMusic.Play();
    }

    public void InSecondBattle()
    {
        FirstBattleMusic.Stop();
        BattleMusic.Play();
    }

    public void InThirdBattle()
    {
        BetweenBattleMusic.Stop();
        BattleMusic.Play();
    }

    public void InSecondRoomBattle()
    {
        BetweenBattleMusic.Stop();
        BattleMusic.Play();
    }

    public void BossBattle()
    {
        BetweenBattleMusic.Stop();
        BattleMusic.Play();
    }

    public void WinScreen()
    {
        
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
                FirstBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;

            case GameState.secondBattle:
                StartCoroutine(StopAndStartMusic( BattleMusic, BetweenBattleMusic));
                SecondBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;

            case GameState.thirdBattle:
                StartCoroutine(StopAndStartMusic(BattleMusic, BetweenBattleMusic));
                ThirdBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;

            case GameState.secondRoomBattle:
                StartCoroutine(StopAndStartMusic(BattleMusic, BetweenBattleMusic));
                SecondRoomBattleOver?.Invoke();
                SwitchState(GameState.betweenBattles);
                break;

            case GameState.bossBattle:
                StartCoroutine(StopAndStartMusic(BattleMusic, BetweenBattleMusic));
                bossBattleOver?.Invoke();
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
