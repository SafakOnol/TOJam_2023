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

    public int damageCounter;   // damagecounter can be used on UI
    public string condition = "Good";
    public bool pickedUp = false;
	public bool vulnerability = false;

    public AudioSource soundBox;
	[SerializeField] public AudioClip boxPickedUp, boxSecured, boxDropped, boxDamaged, boxDestroyed;

	public void Collect()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Box Secured!");
        //Destroy(gameObject);
        // FreezeComponent();
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

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        UnityEngine.Debug.Log("collied obejct is " + collision.transform.tag);
        if (pickedUp && vulnerability)
        {
            damageCounter++;
            // box damage code goes here
            if (damageCounter == 1) { condition = "Cracked"; }
            else if (damageCounter == 2) { condition = "Damaged"; }
            soundBox.PlayOneShot(boxDamaged,0.4f);
        }
    }

    private void DamageToBox()
	{
		vulnerability = true;
	}

    public void PlayChoosenSound(AudioClip clipToPlay)
    {
		soundBox.PlayOneShot(clipToPlay, .8f);
	}


}

