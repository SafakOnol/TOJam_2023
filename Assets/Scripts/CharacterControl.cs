using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
			Debug.Log("neye vurii " + hit.transform.name);
			Debug.Log("neye vurii " + hit.point);
			if (hit.transform.gameObject.tag == "Collectible01")
			{
				// The light color that changes when ray hits a box
				lightToChange.color = Color.green;

				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					if(!pickUp)
					{
						pickUp = true;
						boxToPickUp = hit.transform.gameObject;
						boxToPickUp.GetComponent<Rigidbody>().useGravity = false;
						// change position of the box here to hold point
						boxToPickUp.transform.position = boxHoldPoint.position;
						boxToPickUp.transform.parent = transform;
						boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					}
					/*else
					{
						pickUp = false;
						boxToPickUp.GetComponent<Rigidbody>().useGravity = true;
						boxToPickUp.transform.parent = null;
						boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
						boxToPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
					}*/

					Debug.Log("tekrar tekrar calisiyor mu");

					Debug.Log("box budur : " + boxToPickUp.name);
				}

				// When the ray hit the box code can written here

			}

		}

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if (pickUp)
			{
				pickUp = false;
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

		if (Physics.Raycast(ray, out hitGround, 100))
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
		}
		else
		{
			onground = false;
		}
	}
}
