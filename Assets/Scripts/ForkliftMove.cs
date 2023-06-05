using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForkliftMove : MonoBehaviour
{
    [SerializeField] Transform[] Waypoints;
    [SerializeField] float objectSpeed;
    [SerializeField] float rotationSpeed;
    float rotationTime;

    int TargetWaypointIndex;
    Transform TargetWaypoint;

    Quaternion targetRotation;
    Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Waypoints[0].position;
        TargetWaypoint = Waypoints[1];
        transform.rotation = Quaternion.LookRotation(TargetWaypoint.position - transform.position);        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetDirection = (TargetWaypoint.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Patrol();
    }

    void Patrol()
    {

        if (transform.position == TargetWaypoint.position)
        {
            TargetWaypointIndex++;
            if (TargetWaypointIndex >= Waypoints.Length) { TargetWaypointIndex = 0; }
            TargetWaypoint = Waypoints[TargetWaypointIndex];
        }

        else
        {          
            transform.position = Vector3.MoveTowards(transform.position, TargetWaypoint.position, objectSpeed * Time.deltaTime);
        }

    }  

}
