using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private GameObject Player;
	[SerializeField] private Rigidbody rb;
    [SerializeField] public float speed = 10f;
    [SerializeField] public float jumpForce = 10f;

	[SerializeField] Light lightToChange;
	[SerializeField] private int raycastRange = 10;

	[SerializeField] private Transform boxHoldPoint;
	// [SerializeField] GameObject gameManagerForSound;		// not required for now

	private GameObject boxToPickUp;
	public AudioSource soundBox;
	public AudioClip jumpSound, boxPickedUp, boxSecured, boxDropped, boxDamaged, boxDestroyed;

	public bool	onground = false,
					pickUp = false;


	// Debugging variables
	public TextMeshProUGUI ongroundText;
	public float rayRadius;

	Vector3 lookPos;

	Vector3 offsetRayPos = new Vector3(0, .30f, 0);

	private void Start()
	{
		rb = Player.GetComponent<Rigidbody>();
		soundBox = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
    {
		// Check onground with raycast variables
		Vector3 rayStartPos = transform.position - offsetRayPos;
		Ray groundCheckRay = new Ray(rayStartPos, -transform.up);

		RaycastHit checkGround;

		if (Physics.SphereCast(groundCheckRay, rayRadius, out checkGround, .6f))	// best radius here is .3f
		{
			onground = true;
			// ongroundText.text = "grounded";
		}
		else
		{
			onground = false;
			// ongroundText.text = "air";
		}

		Debug.DrawRay(rayStartPos, -transform.up, Color.white);

		// Will be kept here in case
		// below code can be changed to sphere cast to better check onground
		/*if (Physics.Raycast(groundCheckRay, out checkGround, .6f))
		{
			onground = true;
			// ongroundText.text = "grounded";
		}
		else
		{
			onground = false;
			// ongroundText.text = "air";
		}*/

		
		// box check starts here
		// The light color on standby
		lightToChange.color = Color.red;
		
		Ray rayBoxCheck = new Ray(transform.position + new Vector3(0,-.3f,0), transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(rayBoxCheck, out hit, raycastRange))
		{
			if (hit.transform.gameObject.tag == "Collectible01" || hit.transform.gameObject.tag == "DummyBox")
			{
				// The light color that changes when ray hits a box
				lightToChange.color = Color.green;

				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					if(!pickUp)
					{						
						pickUp = true;
						// pickupText.text = "Picked up";	// on screen test 
						boxToPickUp = hit.transform.gameObject;     // picks the object that is hit by raycast
						PlayChoosenSound(boxPickedUp);              // plays pick up sound each time player picks a box

						if (boxToPickUp.name[0] == 'C')
						{
							boxToPickUp.GetComponent<CargoBox>().pickedUp = true;       // this doesnt work for valuebox - fix it
							boxToPickUp.GetComponent<CargoBox>().Invoke("DamageToBox", 2);
						}
						else if (boxToPickUp.name[0] == 'D')
						{
							boxToPickUp.GetComponent<DummyBox>().pickedUp = true;
							boxToPickUp.GetComponent<DummyBox>().Invoke("DamageToBox", 2);
						}
						else
						{
							boxToPickUp.GetComponent<ValuablesBox>().pickedUp = true;
							boxToPickUp.GetComponent<ValuablesBox>().Invoke("DamageToBox", 2);
						}
					
						boxToPickUp.GetComponent<Rigidbody>().useGravity = false;
						// change position of the box here to hold point						
						boxToPickUp.transform.parent = transform;
						boxToPickUp.transform.position = boxHoldPoint.position;

						boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					}
				}

				// When the ray hit the box code can written here

			}

		}

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if (pickUp)
			{
				pickUp = false;		
				
				PlayChoosenSound(boxDropped);              // plays pick up sound each time player drops a box

				if (boxToPickUp.name[0] == 'C')
				{
					boxToPickUp.GetComponent<CargoBox>().pickedUp = false;
					boxToPickUp.GetComponent<CargoBox>().vulnerability = false;
				}
				else if (boxToPickUp.name[0] == 'D')
				{
					boxToPickUp.GetComponent<DummyBox>().pickedUp = true;
					boxToPickUp.GetComponent<DummyBox>().Invoke("DamageToBox", 2);
				}
				else
				{
					boxToPickUp.GetComponent<ValuablesBox>().pickedUp = false;
					boxToPickUp.GetComponent<ValuablesBox>().vulnerability = false;
				}

				boxToPickUp.GetComponent<Rigidbody>().useGravity = true;
				boxToPickUp.transform.parent = null;
				boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			}
		}

		// box check ends here


		// mouse look character using raycast
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitGround;
		LayerMask layerGorund = 1<<6;

		if (Physics.Raycast(ray, out hitGround, 20000, layerGorund))
		{
			lookPos = hitGround.point;
		}

		Vector3 lookDir = lookPos - transform.position;
		lookDir.y = 0;

		transform.LookAt(transform.position + lookDir, Vector3.up);
		// mouse look ends here


		// Jump code
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		// Run code
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed = 500 * 1.5f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = 500;
		}

	}

	private void FixedUpdate()
	{
		// Controls
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if (direction.magnitude >= 0.1f && onground)
		{
			rb.AddForce(direction * speed, ForceMode.Force);
		}
		else if(!onground){
			rb.AddForce(direction * speed / 10, ForceMode.Force);
		}

	}

	private void Jump()
	{
		if (onground)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			PlayChoosenSound(jumpSound);
		}
	}

	private void OnGroundCheck(Ray groundCheckRay)
	{
		RaycastHit checkGround;

		if (Physics.Raycast(groundCheckRay, out checkGround, .9f))
		{
			onground = true;
			// ongroundText.text = "grounded";
		}
		else
		{
			onground = false;
			// ongroundText.text = "air";
		}
	}

	public void PlayChoosenSound(AudioClip clipToPlay)
	{
		soundBox.PlayOneShot(clipToPlay, .8f);
	}
}
