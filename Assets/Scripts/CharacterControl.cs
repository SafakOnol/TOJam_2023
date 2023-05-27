using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private GameObject Player;
	[SerializeField] private Rigidbody rb;
    [SerializeField] public float speed = 10f;

	private bool onground = false;

    Vector3 mousePos, lookPos;

	private void Start()
	{
		rb = Player.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
		// mouse look character using raycast
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{
			lookPos = hit.point;
		}

		Vector3 lookDir = lookPos - transform.position;
		lookDir.y = 0;

		transform.LookAt(transform.position + lookDir, Vector3.up);

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

	private void OnCollisionEnter(Collision collision)
	{
		onground = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		onground = false;
	}
}
