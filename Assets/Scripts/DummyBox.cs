using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBox : MonoBehaviour
{
	private Rigidbody rb;
	private GameObject playerGameobject;

	public int damageCounter;   // damagecounter can be used on UI
	public string condition = "Good";
	public bool pickedUp = false;
	public bool vulnerability = false;

	public AudioSource soundBox;
	[SerializeField] public AudioClip boxPickedUp, boxSecured, boxDropped, boxDamaged, boxDestroyed;


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

			soundBox.PlayOneShot(boxDamaged, 0.4f);
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
