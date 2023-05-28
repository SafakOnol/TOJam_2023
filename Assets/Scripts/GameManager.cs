using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    NullState,
    MainMenu,
    Intro,
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
    public static event Action OnState_Intro;
    public static event Action OnState_Level01;
    public static event Action OnState_Level01_Special;
    public static event Action OnState_Level01_Post;
    public static event Action OnState_Win;
    public static event Action OnState_GameOver;

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
        SetGameState(GameState.Level01);
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
        Debug.Log("Current Game State: " + CurrentGameState);
        
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
            case GameState.MainMenu:     
                // load main menu
                break;
            case GameState.Intro:
                // Train movement
                // Camera work
                OnState_Intro?.Invoke();
                break;
            case GameState.Level01:
                // set main objective countdown timer
                // game functions
                OnState_Level01?.Invoke();
                break;
            case GameState.Level01_Special:
                // set special objective countdown timer
                // game functions
                OnState_Level01_Special?.Invoke();
                break;
            case GameState.Level01_Post:
                // camera movement
                // ui
                // switch next scene -> this is for later!
                // game functions
                OnState_Level01_Post?.Invoke();
                break;
            case GameState.Win:
                // run win screen
                OnState_Win?.Invoke();
                break;
            case GameState.GameOver:
                // run game over screen
                OnState_GameOver?.Invoke();
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
        SetGameState(GameState.Level01_Special);
        // TODO: set state ...        
    }

    public void CollectorManager_OnAllValuablesBoxCollected()
    {
        Debug.Log("Special Objective Completed!");
        SetGameState(GameState.Win);
        // TODO: set state ...
    }

    //public void CountCollectible()
    //{
    //    //throw new NotImplementedException();
    //    boxCollected++;
    //    Debug.Log(boxCollected);
    //}
}
