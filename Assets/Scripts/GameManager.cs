using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    NullState,
    Intro,
    MainMenu,
    Level01,
    Level01_Special,
    Level01_Post,
    Win,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentGameState;

    public static event Action<GameState> OnGameStateChanged;

    // SoundManagerDebug
    private SoundManager soundManager;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        soundManager = gameObject.GetComponent<SoundManager>();
    }

    private void OnEnable()
    {
        CollectorManager.OnAllCargoBoxesCollected += CollectorManager_OnAllCargoBoxesCollected;
        CollectorManager.OnAllValuablesBoxCollected += CollectorManager_OnAllValuablesBoxCollected;
    }


    private void OnDisable()
    {
        CollectorManager.OnAllCargoBoxesCollected -= CollectorManager_OnAllCargoBoxesCollected;
        CollectorManager.OnAllValuablesBoxCollected -= CollectorManager_OnAllValuablesBoxCollected;
    }

    public void PlayAudio()
    {
        Debug.Log(soundManager.TestSoundManager());
    }

    private void Start()
    {
        // Set initial game state
        //SetGameState(GameState.MainMenu);
        
    }

    public void SetGameState(GameState gameState)
    {
        CurrentGameState = gameState;
        // Handle game state changes
        switch (gameState)
        {
            case GameState.NullState:
                // 
                break;
            case GameState.Intro:
                // 
                break;
            case GameState.MainMenu:     
                // load main menu
                break;
            case GameState.Level01:
                // set countdown timer
                // game functions
                break;
            case GameState.Level01_Special:
                // set countdown timer
                // game functions
                break;
            case GameState.Level01_Post:
                // set countdown timer
                // game functions
                break;
            case GameState.Win:
                // run win screen
                break;
            case GameState.GameOver:
                // run game over screen
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }

        OnGameStateChanged?.Invoke(gameState);

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Add Event listeners here or in other scripts

    

    public void CollectorManager_OnAllCargoBoxesCollected()
    {
        Debug.Log("Main Objective Completed!");
        // TODO: set state ...
    }

    public void CollectorManager_OnAllValuablesBoxCollected()
    {
        Debug.Log("Special Objective Completed!");
        // TODO: set state ...
    }

    //public void CountCollectible()
    //{
    //    //throw new NotImplementedException();
    //    boxCollected++;
    //    Debug.Log(boxCollected);
    //}
}
