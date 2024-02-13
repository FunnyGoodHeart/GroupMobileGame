using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{
    [SerializeField] bool playerUsed;
    [SerializeField] bool enemyUsed;
    [SerializeField] float experationTime = 3f;

    float dissapearTimer = 0f;

    private void Update()
    {
        dissapearTimer += Time.deltaTime;
        if(dissapearTimer >= experationTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Platforms")
        {
            // Bullet is destroyed when it touches a platform
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

        if (collision.gameObject.tag == "Cactus")
        {
            // Ensures that the bullet doesn't get destroyed when used by an enemy
            if (enemyUsed == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
