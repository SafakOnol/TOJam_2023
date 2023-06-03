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
    private bool IsSecured = false;

    public AudioSource soundBox;
	[SerializeField] public AudioClip boxPickedUp, boxSecured, boxDropped, boxDamaged, boxDestroyed;
	public void Collect()
    {
        if(!IsSecured)
        {
            //throw new System.NotImplementedException();
            Debug.Log("Box Secured!");
            //Destroy(gameObject);
            // FreezeComponent();
            IsSecured = true;
            OnCargoBoxSecured?.Invoke();
            pickedUp = true;
        }
        
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
        UnityEngine.Debug.Log("collied obejct is " + collision.transform.tag);
        if (pickedUp && vulnerability)
        {
            damageCounter++;
            // box damage code goes here
            switch (damageCounter)
            {
                case 2:
					condition = "Cracked";
                    playerGameobject.GetComponent<CharacterControl>().PlayChoosenSound(boxDamaged);
					break;
                case 4:
					condition = "Damaged";
					playerGameobject.GetComponent<CharacterControl>().PlayChoosenSound(boxDamaged);
					break;
                case 6:
					playerGameobject.GetComponent<CharacterControl>().PlayChoosenSound(boxDestroyed);
					playerGameobject.GetComponent<CharacterControl>().pickUp = false;
					Destroy(gameObject);
                    break;
			}

            soundBox.PlayOneShot(boxDamaged,0.4f);
        }
    }

	private void DamageToBox()
	{
		if (pickedUp && !vulnerability)
		{
			vulnerability = true;
		}
	}

	public void PlayChoosenSound(AudioClip clipToPlay)
    {
		soundBox.PlayOneShot(clipToPlay, .8f);
	}


}

