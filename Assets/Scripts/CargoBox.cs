using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class CargoBox : MonoBehaviour, ICollectible
{
    public static event Action OnCargoBoxSecured;
    private Rigidbody rb;
    public void Collect()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Box Secured!");
        //Destroy(gameObject);
        FreezeComponent();
        OnCargoBoxSecured?.Invoke();
        
    }

    public void FreezeComponent()
    {
        // Freeze the position of the Rigidbody
        if (TryGetComponent<Rigidbody>(out rb))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the game object.");
        }
    }

}
