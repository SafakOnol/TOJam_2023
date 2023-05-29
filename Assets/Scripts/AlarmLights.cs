using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLights : MonoBehaviour
{
	[SerializeField] private float rotateAngle;
    private bool IsActive = false;
	private void FixedUpdate()
	{
        if (IsActive) transform.Rotate(rotateAngle * Time.deltaTime, 0, 0);
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
