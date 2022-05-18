using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public List<Transform> waypoint = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWayPointIndex;

    public float speed;
    private float rotationSpeed = 2.2f;
    private void Start()
    {
        targetWaypoint = waypoint[targetWaypointIndex];
        lastWayPointIndex = waypoint.Count - 1;
    }
    // Update is called once per frame
    void Update()
    {

        float movement = speed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        CheckDistanceToWayPoint(distance);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movement);

        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);
    }

    void CheckDistanceToWayPoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWayPoint();
        }
    }

    void UpdateTargetWayPoint()
    {
        if (targetWaypointIndex > lastWayPointIndex)
        {
            targetWaypointIndex = 0;
        }
        targetWaypoint = waypoint[targetWaypointIndex];
    }

    //implement_gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        if (waypoint != null && waypoint.Count > 0)
        {
            for (int i = 1; i < waypoint.Count; i++)
            {
                Gizmos.DrawLine(waypoint[i - 1].position, waypoint[i].position);
            }
            Gizmos.DrawLine(waypoint[0].position, waypoint[waypoint.Count - 1].position);
        }
    }
}
