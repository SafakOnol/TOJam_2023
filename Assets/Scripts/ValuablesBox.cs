using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ValuablesBox : MonoBehaviour, ICollectible
{
    public static event Action OnValuablesBoxCollected;
    private Rigidbody rb;

	public bool pickedUp = false,
				vulnerability = false;

	public void Collect()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Valuables Box Secured!");
        //Destroy(gameObject);
        // FreezeComponent();
        OnValuablesBoxCollected?.Invoke();
        
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

	// Invoke by player to remove damage at first pick up
	private void DamageToBox()
	{
		vulnerability = true;
	}

}
