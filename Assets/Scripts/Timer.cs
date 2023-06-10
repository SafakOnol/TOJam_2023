using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private float countdownValue;
    [SerializeField] private float timerValueLevel1;
    [SerializeField] private float timerValueLevel1Special;
    public TextMeshProUGUI timerText;
    private bool timerRunning = false;
    public static event Action OnTimeIsOver;

    private void Awake()
    {
        countdownValue = timerValueLevel1;
    }

    private void OnEnable()
    {
        GameManager.OnState_Level01 += GameManager_OnState_Level01;
        GameManager.OnState_Level01_Special += GameManager_OnState_Level01_Special;
        GameManager.OnState_Win += GameManager_OnState_Win;
    }


    private void OnDisable()
    {
        GameManager.OnState_Level01 -= GameManager_OnState_Level01;
        GameManager.OnState_Level01_Special -= GameManager_OnState_Level01_Special;
    }


    private void GameManager_OnState_Level01()
    {
        //throw new System.NotImplementedException();
        timerRunning = true;
    }
    private void GameManager_OnState_Level01_Special()
    {
        countdownValue = timerValueLevel1Special;
        timerRunning = true;
    }
    private void GameManager_OnState_Win()
    {
        timerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (countdownValue > 0)
            {
                countdownValue -= Time.deltaTime;
            }
            else countdownValue = 0;

            DisplayCountdown(countdownValue);
        }
        
    }

    void DisplayCountdown(float countdownDisplay)
    {
        if (countdownDisplay < 0) 
        {
            countdownDisplay = 0;
            OnTimeIsOver?.Invoke();
        }
        else if (countdownDisplay > 0)
        {
            countdownDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(countdownDisplay / 60);
        float seconds = Mathf.FloorToInt(countdownDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
