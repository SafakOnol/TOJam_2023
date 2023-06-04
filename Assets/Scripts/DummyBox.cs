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
					break;
				case 4:
					condition = "Damaged";
					break;
				case 6:
					playerGameobject.GetComponent<CharacterControl>().pickUp = false;
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
