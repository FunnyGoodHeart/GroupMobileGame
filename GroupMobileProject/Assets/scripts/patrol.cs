using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    //patrol for the tumbleweed so it looks like it is moving around more naturally
    public Transform[] waypoints;
    private int currentWaypointsIndex = 0;
    public float speed = 5f;

    private void Update()
    {
        Vector2 targetPosition = waypoints[currentWaypointsIndex].position;
        transform.position = Vector2.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointsIndex = (currentWaypointsIndex + 1) % waypoints.Length;
        }
    }
}
