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
    Buffer,
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

    //public static event Action<GameState> OnGameStateChanged;
    public static event Action OnState_Buffer;
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

        // ---- INITIAL GAME STATE IS HERE! ---- // 
        SetGameState(GameState.Intro);
        // ---- INITIAL GAME STATE IS HERE! ---- // 
    }

    private void OnEnable()
    {
        TrainMovement.OnTrainArrived += TrainMovement_OnTrainArrived;
        CollectorManager.OnAllCargoBoxesCollected += CollectorManager_OnAllCargoBoxesCollected;
        CollectorManager.OnAllValuablesBoxCollected += CollectorManager_OnAllValuablesBoxCollected;
    }


    private void OnDisable()
    {
        TrainMovement.OnTrainArrived += TrainMovement_OnTrainArrived;
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
        //Debug.Log("Current Game State: " + CurrentGameState);
        
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
                Debug.Log("Current Game State: " + CurrentGameState);
                break;
            case GameState.Buffer:
                Debug.Log("Current Game State: " + CurrentGameState);
                // fake loading screen for 3 seconds
                OnState_Buffer?.Invoke();
                break;
            case GameState.Intro:
                Debug.Log("Current Game State: " + CurrentGameState);
                // Train movement
                // Camera work
                // 
                OnState_Intro?.Invoke();
                PopUpTexts.DisplayPopUp(PopUps.INTRO, gameObject);
                break;
            case GameState.Level01:
                Debug.Log("Current Game State: " + CurrentGameState);
                // set main objective countdown timer
                // game functions
                OnState_Level01?.Invoke();
                PopUpTexts.DisplayPopUp(PopUps.OBJECTIVE1, gameObject);
                break;
            case GameState.Level01_Special:
                Debug.Log("Current Game State: " + CurrentGameState);
                // set special objective countdown timer
                // game functions
                OnState_Level01_Special?.Invoke();
                PopUpTexts.DisplayPopUp(PopUps.OBJECTIVE2, gameObject);
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
                PopUpTexts.DisplayPopUp(PopUps.WIN, gameObject);
                Invoke("WinConditionLoadManager", 6);
				break;
            case GameState.GameOver:
                // run game over screen
                OnState_GameOver?.Invoke();
                PopUpTexts.DisplayPopUp(PopUps.GAMEOVER, gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }

        //OnGameStateChanged?.Invoke(gameState);

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

    
    private void TrainMovement_OnTrainArrived()
    {
        Debug.Log("Train has arrived!");
        SetGameState(GameState.Level01);
    }

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

	public void WinConditionLoadManager()
	{
        LoadScene("MenuScene");
	}
}
