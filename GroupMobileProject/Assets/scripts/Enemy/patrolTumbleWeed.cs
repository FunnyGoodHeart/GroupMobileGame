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
    [SerializeField] bool randomJumpingActive = true; //if the caracter sould randomly jump or not
    [SerializeField] float jumpForceMin = 5f;
    [SerializeField] float jumpForceMax = 10f;
    [SerializeField] float jumpInterval = 0.5f;
    float lastJumpTime = 0f;

    //stuff for chasing the player
    [Header("chasing Player")]
    [SerializeField] bool chasePlayerActive = true; //if false then character will not follow player
    [SerializeField] Transform playerTransform;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float patrolRange = 10f;
    bool isChasingPlayer = false;
    Vector2 currentMovementDirection = Vector2.zero;
    private void Update()
    {
        if (isChasingPlayer && chasePlayerActive)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
       //random jumping to make it seem like it is with the wind
        if(Time.time - lastJumpTime > jumpInterval && randomJumpingActive)
        {
            lastJumpTime = Time.time;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(jumpForceMin, jumpForceMax), ForceMode2D.Impulse);
        }
        
    }
    private void Patrol()
    {
        //follows patrol unless player get too close
        Vector2 targetPosition = waypoints[currentWaypointsIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointsIndex = (currentWaypointsIndex + 1) % waypoints.Length;
        }

        if(Vector2.Distance(transform.position, playerTransform.position) < chaseRange)
        {
            isChasingPlayer = true;
        }
    }
    private void ChasePlayer()
    {
        //follows player unless it if player gets too far away or if the enemy get too far away from the patrol
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseRange || Vector2.Distance(transform.position, waypoints[currentWaypointsIndex].position) > patrolRange)
        {
            isChasingPlayer = false;
        }
        
    }
}
