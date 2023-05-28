using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FunctionTimer.Create(TestAction, 3f, "Timer");
        FunctionTimer.Create(TestAction02, 5f, "Timer 02");

        FunctionTimer.StopTimer("Timer");
    }


    private void TestAction()
    {
        Debug.Log("Test Timer Function!");
    }

    private void TestAction02()
    {
        Debug.Log("Test Timer Function 02!");
    }
    
}
