using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CargoBox : MonoBehaviour, ICollectible
{
    public static event Action OnCargoBoxSecured;
    public void Collect()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Box Secured!");
        Destroy(gameObject);
        OnCargoBoxSecured?.Invoke();
    }

    
}
