using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolTumbleWeed : MonoBehaviour
{
    //patrol for the tumbleweed so it looks like it is moving around more naturally
    public Transform[] waypoints;
    private int currentWaypointsIndex = 0;
    [SerializeField] float speed = 5f;



    //do small jumps randomly while walking around
    [Header("random Jumping Range")]
    [SerializeField] bool randomJumpingActive = true;
    [SerializeField] float jumpForceMin = 5f;
    [SerializeField] float jumpForceMax = 10f;
    [SerializeField] float jumpInterval = 0.5f;
    float lastJumpTime = 0f;

    private void Update()
    {

        Vector2 targetPosition = waypoints[currentWaypointsIndex].position;
        transform.position = Vector2.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);
        if(Time.time - lastJumpTime > jumpInterval && randomJumpingActive)
        {
            lastJumpTime = Time.time;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(jumpForceMin, jumpForceMax), ForceMode2D.Impulse);
        }
        if(Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointsIndex = (currentWaypointsIndex + 1) % waypoints.Length;
        }
    }
}
