using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int plHealth = 10;
    [SerializeField] attackTumbleweed atkTums;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTums.tumsTriggerAtk)
        {
            plHealth -= atkTums.atkTumbleweed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTums.tumsCollisionAtk)
        {
            plHealth -= atkTums.atkTumbleweed;
        }
    }
}
