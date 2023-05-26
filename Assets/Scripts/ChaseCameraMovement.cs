using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCameraMovement : MonoBehaviour
{
	[SerializeField] private Transform PlayersTransform;
	[SerializeField] private float smoothTime;
	private Vector3 _offset;
	private Vector3 _currentVelocity = Vector3.zero;

	private void Awake()
	{
		_offset = new Vector3(0, 8, 0);
		transform.position = PlayersTransform.position + _offset;

		// Rotate the cube by converting the angles into a quaternion.
		Quaternion target = Quaternion.Euler(90, 0, 0);

		// Dampen towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
	}

	private void LateUpdate()
	{
		Vector3 targetPosition = PlayersTransform.position + _offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);


	}
}
