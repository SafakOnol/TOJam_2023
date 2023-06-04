using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZoneOpen : MonoBehaviour
{
    bool IsActive = true;
    private void FixedUpdate()
    {
        if (!IsActive) Destroy(gameObject);
    }

    private void OpenLoadingZone()
    {
        IsActive = false;
    }

    private void OnEnable()
    {
        GameManager.OnState_Level01 += OpenLoadingZone;
    }

    private void OnDisable()
    {
        GameManager.OnState_Level01 -= OpenLoadingZone;
    }


}
