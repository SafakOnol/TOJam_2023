using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PopUpTexts : MonoBehaviour
{
    public static bool isActive { get; private set; }

    public static GameObject intro, objective1, objective2, win, gameover, mainMenuObject;

    public static void Init()
    {
        Debug.Log("popuptext Init called!");
        GameObject canvas = GameObject.Find("PopUpNotifications");
        intro = canvas.transform.Find("Intro").gameObject;
        objective1 = canvas.transform.Find("Objective1").gameObject;
        objective2 = canvas.transform.Find("Objective2").gameObject;
        win = canvas.transform.Find("Win").gameObject;
        gameover = canvas.transform.Find("GameOver").gameObject;

        isActive = false;
    }

    public static void DisplayPopUp(PopUps aPopUpItem, GameObject displayingCanvas)
    {
        if (!isActive) Init();

        switch (aPopUpItem)
        {
            case PopUps.INTRO:
                intro.SetActive(true);
                //FunctionTimer.Create(StopIntroPopUp, 5f, "StopDisplayPopUp");
                break;
            case PopUps.OBJECTIVE1:
                objective1.SetActive(true);
                //FunctionTimer.Create(StopObjective1PopUp, 5f, "StopObjective1PopUp");
                break;
            case PopUps.OBJECTIVE2:
                objective2.SetActive(true);
                //FunctionTimer.Create(StopObjective2PopUp, 5f, "StopObjective2PopUp");
                break;
            case PopUps.WIN:
                win.SetActive(true);
                //FunctionTimer.Create(StopObjective2PopUp, 5f, "WIN");
                break;
            case PopUps.GAMEOVER:
                gameover.SetActive(true);
                //FunctionTimer.Create(StopObjective2PopUp, 5f, "GAMEOVER");
                break;
            default: break;
        }
        //displayingCanvas.SetActive(false);
    }

    public static void DelayedStopIntroPopUp()
    {
        FunctionTimer.Create(StopIntroPopUp, 5f, "StopIntroPopUp");
    }

    public static void StopIntroPopUp()
    {
        StopDisplayPopUp(PopUps.INTRO, intro);
        //intro.SetActive(false);
    }

    public static void StopObjective1PopUp()
    {
        StopDisplayPopUp(PopUps.OBJECTIVE1, objective1);
        //objective1.SetActive(false);
    }

    public static void StopObjective2PopUp()
    {
        StopDisplayPopUp(PopUps.OBJECTIVE2, objective2);
        //objective2.SetActive(false);
    }

    public static void StopWinPopUp()
    {
        StopDisplayPopUp(PopUps.WIN, win);
        win.SetActive(false);
        MenuManager.mainMenu.SetActive(true);
    }

    public static void StopGameOverPopUp()
    {
        StopDisplayPopUp(PopUps.GAMEOVER, gameover);
        gameover.SetActive(false);
        MenuManager.mainMenu.SetActive(true);
    }

    public static void StopDisplayPopUp(PopUps aPopUpItem, GameObject displayingCanvas)
    {
        switch (aPopUpItem)
        {
            case PopUps.INTRO:
                intro.SetActive(false);
                break;
            case PopUps.OBJECTIVE1:
                objective1.SetActive(false);
                break;
            case PopUps.OBJECTIVE2:
                objective2.SetActive(false);
                break;
            default: break;
        }
    }
}

public enum PopUps
{
    INTRO,
    OBJECTIVE1,
    OBJECTIVE2,
    WIN,
    GAMEOVER    
}
