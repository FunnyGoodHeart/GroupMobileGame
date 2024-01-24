using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTumbleweed : MonoBehaviour
{
    [SerializeField] int tumsHealths = 10;
    [SerializeField] playerAttack plAtk;
    void Start()
    {
        
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
