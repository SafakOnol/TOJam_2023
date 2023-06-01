using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLights : MonoBehaviour
{
	[SerializeField] private float rotateAngle;
    private Vector3 TargetPosition;
    private bool IsActive = false;
    private Vector3 _offset = new Vector3(0f, 1000f, 0f);

    private void FixedUpdate()
	{
        if (IsActive)
        {
            transform.position = TargetPosition;
            transform.Rotate(rotateAngle * Time.deltaTime, 0, 0);
            Debug.Log("TargetPosition: "+ TargetPosition);
        }

	}

    private void Awake()
    {
        TargetPosition = transform.position;
        transform.position += _offset;
    }

    private void OnEnable()
    {
        GameManager.OnState_Level01_Special += GameManager_OnState_Level01_Special;
    }

    private void OnDisable()
    {
        GameManager.OnState_Level01_Special -= GameManager_OnState_Level01_Special;
    }

    private void GameManager_OnState_Level01_Special()
    {
        IsActive = true;
    }
}
