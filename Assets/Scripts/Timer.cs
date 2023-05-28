using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private static float countdownValue = 100;
    public TextMeshProUGUI timerText;
    
    // Update is called once per frame
    void Update()
    {
        if (countdownValue > 0) 
        {
            countdownValue -= Time.deltaTime;        
        }
        else countdownValue = 0;

        DisplayCountdown(countdownValue);
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
