using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ValuablesBox : MonoBehaviour, ICollectible
{
    public static event Action OnValuablesBoxCollected;
    public static event Action OnDamage;
    private Rigidbody rb;

    public int damageCounter;   // damagecounter can be used on UI
    public string condition = "Good";
    public bool pickedUp = false;
    public bool vulnerability = false;
    private bool IsSecured = false;

    public AudioSource soundBox;
    [SerializeField] public AudioClip boxPickedUp, boxSecured, boxDropped, boxDamaged, boxDestroyed;
    public void Collect()
    {
        if (!IsSecured)
        {
            //throw new System.NotImplementedException();
            Debug.Log("Valuables Box Secured!");
            //Destroy(gameObject);
            // FreezeComponent();
            IsSecured = true;
            OnValuablesBoxCollected?.Invoke();            
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

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        UnityEngine.Debug.Log("collied obejct is " + collision.transform.tag);
        if (pickedUp && vulnerability)
        {
            damageCounter++;
            // box damage code goes here
            if (damageCounter == 1) { condition = "Cracked"; }
            else if (damageCounter == 2) { condition = "Damaged"; }
            soundBox.PlayOneShot(boxDamaged, 0.4f);
        }
    }
    public void PlayChoosenSound(AudioClip clipToPlay)
    {
        soundBox.PlayOneShot(clipToPlay, .8f);
    }

}
