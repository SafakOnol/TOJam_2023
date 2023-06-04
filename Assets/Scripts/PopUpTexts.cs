using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;

public class PopUpTexts : MonoBehaviour
{
    public static bool isActive { get; private set; }

    public static GameObject intro, objective1, objective2, win, gameover;
    public static void Init()
    {
        GameObject canvas = GameObject.Find("PopUpNotifications");
        intro = canvas.transform.Find("Intro").gameObject;
        objective1 = canvas.transform.Find("Objective1").gameObject;
        objective2 = canvas.transform.Find("Objective2").gameObject;
        win = canvas.transform.Find("Win").gameObject;
        gameover = canvas.transform.Find("GameOver").gameObject;
    }

    private void OnEnable()
    {
        GameManager.OnState_Intro += DisplayIntroPopUps;
        GameManager.OnState_Level01 += DisplayLevel01PopUps;
        GameManager.OnState_Level01_Special += DisplayLevel01SpecialPopUps;
        GameManager.OnState_Win += DisplayWinPopUps;
        GameManager.OnState_GameOver += DisplayGameOverPopUps;
    }

    private void OnDisable()
    {
        GameManager.OnState_Intro -= DisplayIntroPopUps;
        GameManager.OnState_Level01 -= DisplayLevel01PopUps;
        GameManager.OnState_Level01_Special -= DisplayLevel01SpecialPopUps;
        GameManager.OnState_Win -= DisplayWinPopUps;
        GameManager.OnState_GameOver -= DisplayGameOverPopUps;
    }

    private void Awake()
    {
        if (!isActive) Init();
    }

    private void DisplayGameOverPopUps()
    {
        gameover.SetActive(true);

        FunctionTimer.Create(StopGameOverPopUp, 5f, "StopGameOverPopUp");
    }

    private void DisplayWinPopUps()
    {
        win.SetActive(true);
        FunctionTimer.Create(StopWinPopUp, 5f, "StopWinPopUp");
    }

    private void DisplayLevel01SpecialPopUps()
    {
        objective2.SetActive(true);
        FunctionTimer.Create(StopObjective2PopUp, 5f, "StopObjective2PopUp");
    }

    private void DisplayLevel01PopUps()
    {
        objective1.SetActive(true);
        FunctionTimer.Create(StopObjective1PopUp, 5f, "StopObjective1PopUp");
    }

    private void DisplayIntroPopUps()
    {
        intro.SetActive(true);
        FunctionTimer.Create(StopIntroPopUp, 5f, "StopDisplayPopUp");
    }

    //public static void DisplayPopUp(PopUps aPopUpItem, GameObject displayingCanvas)
    //{
    //    if (!isActive) Init();

    //    switch (aPopUpItem)
    //    {
    //        case PopUps.INTRO:
    //            intro.SetActive(true);
    //            FunctionTimer.Create(StopIntroPopUp, 5f, "StopDisplayPopUp");
    //            break;
    //        case PopUps.OBJECTIVE1:
    //            objective1.SetActive(true);
    //            FunctionTimer.Create(StopObjective1PopUp, 5f, "StopObjective1PopUp");
    //            break;
    //        case PopUps.OBJECTIVE2:
    //            objective2.SetActive(true);
    //            FunctionTimer.Create(StopObjective2PopUp, 5f, "StopObjective2PopUp");
    //            break;
    //        case PopUps.WIN:
    //            win.SetActive(true);
    //            FunctionTimer.Create(StopObjective2PopUp, 5f, "WIN");
    //            break;
    //        case PopUps.GAMEOVER:
    //            gameover.SetActive(true);
    //            FunctionTimer.Create(StopObjective2PopUp, 5f, "GAMEOVER");
    //            break;
    //        default: break;
    //    }
    //    //displayingCanvas.SetActive(false);
    //}

    public static void StopIntroPopUp()
    {
        intro.SetActive(false);
    }

    public static void StopObjective1PopUp()
    {
        objective1.SetActive(false);
    }

    public static void StopObjective2PopUp()
    {
        objective2.SetActive(false);
    }

    public static void StopWinPopUp()
    {
        win.SetActive(false);
    }

    public static void StopGameOverPopUp()
    {
        gameover.SetActive(false);
    }
    //public static void StopDisplayPopUp(PopUps aPopUpItem, GameObject displayingCanvas)
    //{
    //    switch (aPopUpItem)
    //    {
    //        case PopUps.INTRO:
    //            intro.SetActive(false);
    //            break;
    //        case PopUps.OBJECTIVE1:
    //            objective1.SetActive(false);
    //            break;
    //        case PopUps.OBJECTIVE2:
    //            objective2.SetActive(false);
    //            break;
    //        default: break;
    //    }
    //}
}

public enum PopUps
{
    INTRO,
    OBJECTIVE1,
    OBJECTIVE2,
    WIN,
    GAMEOVER    
}
