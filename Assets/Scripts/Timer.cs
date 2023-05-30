using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float countdownValue;
    [SerializeField] private float timerValue;
    public TextMeshProUGUI timerText;
    private bool timerRunning = false;

    private void Awake()
    {
        countdownValue = timerValue;
    }

    private void OnEnable()
    {
        GameManager.OnState_Level01 += GameManager_OnState_Level01;
    }

    private void OnDisable()
    {
        GameManager.OnState_Level01 -= GameManager_OnState_Level01;
    }


    private void GameManager_OnState_Level01()
    {
        //throw new System.NotImplementedException();
        timerRunning = true;
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
