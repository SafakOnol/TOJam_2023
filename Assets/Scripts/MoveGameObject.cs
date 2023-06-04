using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObject : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] Transform[] Rotations;
    [SerializeField] float objectSpeed;

    int TargetPositionIndex;

    Transform TargetPosition;
    Transform TargetRotation;
    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = Positions[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Patrol();
        Move();
    }

    //void Patrol()
    //{
    //    if (transform.position == TargetPosition.position)
    //    {
    //        TargetPositionIndex++;
    //        if (TargetPositionIndex >= Positions.Length) { TargetPositionIndex = 0; }
    //        TargetPosition = Positions[TargetPositionIndex];
    //    }

    //    else
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, TargetPosition.position, objectSpeed * Time.deltaTime);
    //    }

    //}

    void Move()
    {
        if (transform.position == TargetPosition.position)
        {
            TargetPositionIndex++;
            if (TargetPositionIndex >= Positions.Length) { TargetPositionIndex = Positions.Length; }
            TargetPosition = Positions[TargetPositionIndex];
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition.position, objectSpeed * Time.deltaTime);
        }

    }
}
