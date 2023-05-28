using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCameraMovement : MonoBehaviour
{
	[SerializeField] private Transform PlayersTransform;
	[SerializeField] private float smoothTime;
	[SerializeField] private float angleToChangeCamRotation;
	private Vector3 _offset;
	private Vector3 _currentVelocity = Vector3.zero;

	private void Awake()
	{
		// we set offset here manually
		//_offset = new Vector3(0, 5, -5);

		// or we can set the difference as offset
		_offset = transform.position - PlayersTransform.position;
		transform.position = PlayersTransform.position + _offset;

		// Rotate the cube by converting the angles into a quaternion.
		Quaternion target = Quaternion.Euler(angleToChangeCamRotation, 0, 0);

		// Dampen towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
	}

	private void LateUpdate()
	{
		Vector3 targetPosition = PlayersTransform.position + _offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
	}
}
