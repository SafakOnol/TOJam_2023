using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    private static List<FunctionTimer> activeTimersList; // References of all active timers
    private static GameObject initGameObject; // a global game object to initialize class, destroyed once the scene ends or changes
    private static void InitIfNeeded()
    {
        if (initGameObject == null) 
        {
            initGameObject = new GameObject("FunctionTimer_InitGameObject");
            activeTimersList = new List<FunctionTimer>();
        }
    }
    public static FunctionTimer Create(Action action, float timer, string functionName = null)
    {
        InitIfNeeded();
        GameObject gameObject = new GameObject("FunctionTimer Object"+ functionName, typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(gameObject, action, timer, functionName);        
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;
        activeTimersList.Add(functionTimer);

        return functionTimer;
    }

    private static void RemoveTimer(FunctionTimer functionTimer)
    {
        InitIfNeeded();
        activeTimersList.Remove(functionTimer);
    }

    public static void StopTimer(string functionName)
    {
        for (int i = 0; i < activeTimersList.Count; i++)
        {
            if (activeTimersList[i].functionName == functionName)
            {
                // stop the timer!
                activeTimersList[i].DestroySelf();
                i--;
            }
        }    
    }


    // Dummy class to access Monobehaviour functions
    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            onUpdate?.Invoke();
        }
    }

    private Action action;
    private float timer;
    private GameObject gameObject;
    private readonly string functionName;
    private bool isDestroyed;

    // This constructor is private and can be called within this class only
    private FunctionTimer(GameObject gameObject, Action action, float timer, string functionName)
    {
        this.action = action;
        this.timer = timer;
        this.functionName = functionName;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;            
        }

        if (this.timer < 0)
        {
            action();
            DestroySelf();
        }

    }

    private void DestroySelf()
    {
        RemoveTimer(this);
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        
    }


    // A Class to trigger actions manually without creating a GameObject
    
    public class FunctionTimerObject
    {
        private float timer;
        private Action callback;
        
        public FunctionTimerObject(Action callback,  float timer)
        {
            this.callback = callback;
            this.timer = timer;
        }

        public void Update()
        {
            Update(Time.deltaTime);
        }

        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer < 0)
            {
                callback();
            }    
        }

        // Create an object that must be manually updated thru Update()
        public static FunctionTimerObject CreateObject(Action callback, float timer)
        {
            return new FunctionTimerObject(callback, timer);
        }

    }
}
