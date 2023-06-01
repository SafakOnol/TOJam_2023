using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] float trainSpeed;

    private int TrainTargetPositionIndex;
    Transform TrainTargetPosition;
    private bool IsLoading = false;

    public static event Action OnTrainArrived;

    void Start()
    {
        TrainTargetPosition = Positions[0];
    }

    void FixedUpdate()
    {
        MoveTrain();
    }

    void MoveTrain()
    {
        if (!IsLoading) 
        {
            if (transform.position == TrainTargetPosition.position)
            {
                TrainTargetPositionIndex++;
                if (TrainTargetPositionIndex >= Positions.Length)
                {
                    TrainTargetPositionIndex -= 1;
                    IsLoading = true;
                    OnTrainArrived?.Invoke();
                }
                TrainTargetPosition = Positions[TrainTargetPositionIndex];
            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, TrainTargetPosition.position, trainSpeed * Time.deltaTime);
            }
        }
        

    }
}
