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
	public TextMeshProUGUI pickupText;
	public TextMeshProUGUI ongroundText;

	private GameObject boxToPickUp;

	private bool	onground = false,
					pickUp = false;

    Vector3 lookPos;

	Vector3 offsetRayPos = new Vector3(0, .65f, 0);

	private void Start()
	{
		rb = Player.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
		// Check onground with raycast variables
		Vector3 rayStartPos = transform.position - offsetRayPos;
		Ray groundCheckRay = new Ray(rayStartPos, -transform.up);

		// Check onground with raycast
		OnGroundCheck(groundCheckRay);

		// box check starts here
		// The light color on standby
		lightToChange.color = Color.red;

		
		Ray rayBoxCheck = new Ray(transform.position + new Vector3(0,-.3f,0), transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(rayBoxCheck, out hit, raycastRange))
		{
			if (hit.transform.gameObject.tag == "Collectible01" || hit.transform.gameObject.tag == "DummyBox")
			if (hit.transform.gameObject.tag == "Collectible01")
			{
				// The light color that changes when ray hits a box
				lightToChange.color = Color.green;

				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					if(!pickUp)
					{
						pickUp = true;
						pickupText.text = "Picked up";	// on screen test 
						boxToPickUp = hit.transform.gameObject;
						CargoBox boxScript = boxToPickUp.GetComponent<CargoBox>();
							// sound for pickup
							boxScript.PlayChoosenSound(boxScript.boxPickedUp);
						boxToPickUp.GetComponent<CargoBox>().pickedUp = true;
						boxToPickUp.GetComponent<CargoBox>().Invoke("DamageToBox", 2);						
						boxToPickUp.GetComponent<Rigidbody>().useGravity = false;
						// change position of the box here to hold point
						boxToPickUp.transform.position = boxHoldPoint.position;
						boxToPickUp.transform.parent = transform;
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
				pickupText.text = "Not Picked up";  // on screen test 
				CargoBox boxScript = boxToPickUp.GetComponent<CargoBox>();
				// sound for pickup
				boxScript.PlayChoosenSound(boxScript.boxDropped);
				boxToPickUp.GetComponent<CargoBox>().pickedUp = false;
				boxToPickUp.GetComponent<CargoBox>().vulnerability = false;
				boxToPickUp.GetComponent<Rigidbody>().useGravity = true;
				boxToPickUp.transform.parent = null;
				boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			}
		}

		// box check ends here

		// box to hold movement script
		// ends here


		// mouse look character using raycast
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitGround;
		LayerMask layerGorund = 1<<6;

		if (Physics.Raycast(ray, out hitGround, 100, layerGorund))
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
			speed = 350 * 1.5f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = 350;
		}

	}

	private void FixedUpdate()
	{
		// Run code


		// Controls
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if (direction.magnitude >= 0.1f && onground)
		{
			rb.AddForce(direction * speed, ForceMode.Force);
		}
		else if(!onground){
			rb.AddForce(direction * speed / 5, ForceMode.Force);
		}

	}

	private void Jump()
	{
		if (onground)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void OnGroundCheck(Ray groundCheckRay)
	{
		RaycastHit checkGround;

		if (Physics.Raycast(groundCheckRay, out checkGround, .9f))
		{
			onground = true;
			ongroundText.text = "grounded";
		}
		else
		{
			onground = false;
			ongroundText.text = "air";
		}
	}
}
