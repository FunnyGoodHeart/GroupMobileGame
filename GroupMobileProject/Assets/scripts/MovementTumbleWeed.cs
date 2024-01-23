using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TumbleWeedMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float chaseDistance = 2.0f;
    [SerializeField] float moveSpeed = 5.0f;
    Vector3 home;
    private void Start()
    {
        home = transform.position;
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
            GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
        }
        else
        {
            moveDirection = home - transform.position;
            if (moveDirection.magnitude >= 0.5f)
            {
                moveDirection.Normalize();
                GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
            }
            // GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            else
            {
                //close enough
                transform.position = home;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }

    }
}
