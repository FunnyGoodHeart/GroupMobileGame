using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTumbleweed : MonoBehaviour
{
    [SerializeField] int tumsHealths = 10;
     playerAttack plAtk;
    [SerializeField] bool dropsItem = false;
    [SerializeField] GameObject player;
    [SerializeField] GameObject itemDropped;

    private void Start()
    {
        plAtk = player.GetComponent<playerAttack>(); ;
    }
    void Update()
    {
        if(tumsHealths <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Bullet" && plAtk.plTriggerAtk)
        {
            tumsHealths -= plAtk.playerAtk;

            if (tumsHealths <= 0)
            {
                if (dropsItem == true)
                {
                    GameObject item = Instantiate(itemDropped, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && plAtk.plCollisionAtk)
        {
            tumsHealths -= plAtk.playerAtk;
        }
    }
}
