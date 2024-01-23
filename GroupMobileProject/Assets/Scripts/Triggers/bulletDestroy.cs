using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{
    [SerializeField] bool playerUsed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (playerUsed == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
