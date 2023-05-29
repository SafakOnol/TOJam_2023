using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAlarmLights : MonoBehaviour
{
    [SerializeField] private float rotateAngle;
    private void FixedUpdate()
    {
        transform.Rotate(rotateAngle * Time.deltaTime, 0, 0);
    }

}
