using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ValuablesBox : MonoBehaviour, ICollectible
{
    public static event Action OnValuablesBoxCollected;
    public static event Action OnDamage;
    private Rigidbody rb;
	private GameObject playerGameobject;

	public int damageCounter;   // damagecounter can be used on UI
	public string condition = "Good";
	public bool pickedUp = false,
				vulnerability = false;

	public bool IsSecured = false;


	public void Collect()
    {
        if (!IsSecured)
        {
            //throw new System.NotImplementedException();
            Debug.Log("Valuables Box Secured!");
            
            // FreezeComponent();
            IsSecured = true;
            OnValuablesBoxCollected?.Invoke();
            //FunctionTimer.Create(DestroyAfterCollected, 5f, "DestoyAfterCollected");
            //Destroy(gameObject);
        }

    }

    public void DestroyAfterCollected()
    {
        Destroy(gameObject);
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

	private void Awake()
	{
		playerGameobject = GameObject.FindGameObjectWithTag("Player");
	}

	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
		//UnityEngine.Debug.Log("collied obejct is " + collision.transform.tag);
		if (pickedUp && vulnerability)
		{
			damageCounter++;
			// box damage code goes here
			switch (damageCounter)
			{
				case 2:
					playerGameobject.gameObject.GetComponent<CharacterControl>().PlayChoosenSound(playerGameobject.GetComponent<CharacterControl>().boxDamaged);
					condition = "Damaged";
					break;
				case 4:
					playerGameobject.gameObject.GetComponent<CharacterControl>().PlayChoosenSound(playerGameobject.GetComponent<CharacterControl>().boxDestroyed);
					playerGameobject.GetComponent<CharacterControl>().pickUp = false;
					Destroy(gameObject);
					break;
			}
		}
	}

	// Invoke by player to remove damage at first pick up
	private void DamageToBox()
	{
		if(pickedUp && !vulnerability)
		{
			vulnerability = true;
		}
	}

}
