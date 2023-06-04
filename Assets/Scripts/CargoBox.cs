using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;



public class CargoBox : MonoBehaviour, ICollectible
{
    public static event Action OnCargoBoxSecured;
    public static event Action OnDamage;
    private Rigidbody rb;
	private GameObject playerGameobject;

	public int damageCounter;   // damagecounter can be used on UI
    public string condition = "Good";
    public bool pickedUp = false;
	public bool vulnerability = false;
    public bool IsSecured = false;

	public void Collect()
    {
        if(!IsSecured)
        {
            //throw new System.NotImplementedException();
            Debug.Log("Box Secured!");
            
            // FreezeComponent();
            IsSecured = true;
            OnCargoBoxSecured?.Invoke();
            //FreezeComponent();
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
					condition = "Cracked";
                    playerGameobject.gameObject.GetComponent<CharacterControl>().PlayChoosenSound(playerGameobject.GetComponent<CharacterControl>().boxDamaged);
                    break;
                case 4:
					condition = "Damaged";
                    playerGameobject.gameObject.GetComponent<CharacterControl>().PlayChoosenSound(playerGameobject.GetComponent<CharacterControl>().boxDamaged);
                    break;
                case 6:
                    playerGameobject.gameObject.GetComponent<CharacterControl>().PlayChoosenSound(playerGameobject.GetComponent<CharacterControl>().boxDestroyed);
                    playerGameobject.GetComponent<CharacterControl>().pickUp = false;
                    UI_PickUp.StopDisplay(PickUpItems.CARGOBOX, gameObject);
                    playerGameobject.GetComponent<CharacterControl>().animator.SetBool("isPickedUp", false);
					Destroy(gameObject);
                    break;
			}

        }
    }

	private void DamageToBox()
	{
		if (pickedUp && !vulnerability)
		{
			vulnerability = true;
		}
	}

}

