using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAreaLights : MonoBehaviour
{
    [SerializeField] private float rotateAngle;
    private bool IsActive = false;
    private void FixedUpdate()
    {
        if (IsActive) transform.Rotate(rotateAngle * Time.deltaTime, 0, 0);
    }

    private void OnEnable()
    {
        GameManager.OnState_Level01 += GameManager_OnState_Level01;
    }

    private void OnDisable()
    {
        GameManager.OnState_Level01 -= GameManager_OnState_Level01;
    }

    private void GameManager_OnState_Level01()
    {
        IsActive = true;
    }
}
