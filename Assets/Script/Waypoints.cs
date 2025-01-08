using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public float speed;
    public List<Transform> waypoints;
    private int TargetIndex = 0;
    private Transform targetwaypoints;
    // Start is called before the first frame update
    void Start()
    {
        targetwaypoints = waypoints[TargetIndex];

    }

    // Update is called once per frame
    void Update()
    {
        FollowWaypoint(targetwaypoints);
        CheckArrival();

    }
    private void CheckArrival()
    {
        float distance = Vector3.Distance(transform.position, targetwaypoints.position);
        if (distance < 0.1f){
            targetwaypoints = GetNextTargetWaypoint();
        }
    }
    private void FollowWaypoint(Transform waypoint)
    {
        Vector3 direction = targetwaypoints.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, Mathf.PI * 2 / 36, Mathf.PI * 2);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private Transform GetNextTargetWaypoint()
    {
        if (TargetIndex < waypoints.Count - 1)
        {
            TargetIndex++;
        }
        else
        {
            TargetIndex = 0;
        }
            return waypoints[TargetIndex];
    }

}
