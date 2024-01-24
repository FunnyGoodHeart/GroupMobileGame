using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TumbleWeedMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float chaseDistance = 2.0f;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] public bool playerIsInRange = false;
    Vector3 home;
    Rigidbody2D enemyRB;
    private void Start()
    {
        home = transform.position;
        enemyRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        //calculate the distance between the enemy and the player by Destination-Start
        Vector3 moveDirection = playerPosition - transform.position;
        //if the player is close
        if (moveDirection.magnitude < chaseDistance)
        {
            moveDirection.Normalize();
            enemyRB.velocity = moveDirection * moveSpeed;
        }
        else
        {
            moveDirection = home - transform.position;
            if (moveDirection.magnitude >= 0.5f)
            {
                moveDirection.Normalize();
                enemyRB.velocity = moveDirection * moveSpeed;
            }
            // GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            else
            {
                //close enough
                transform.position = home;
                enemyRB.velocity = Vector3.zero;
            }
        }

    }
}
