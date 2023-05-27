using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ValuablesBox : MonoBehaviour, ICollectible
{
    public static event Action OnValuablesBoxCollected;
    public void Collect()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Valuables Box Secured!");
        Destroy(gameObject);
        OnValuablesBoxCollected?.Invoke();
    }
}
