using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{
    [SerializeField] bool playerUsed;
    [SerializeField] bool enemyUsed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            // Ensures that the bullet doesn't get destroyed when used by the player
            if (playerUsed == false)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "TumbleWeed")
        {
            // Ensures that the bullet doesn't get destroyed when used by an enemy
            if (enemyUsed == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
