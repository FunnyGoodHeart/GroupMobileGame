using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRacoonHealth : MonoBehaviour
{
    [SerializeField] int racoonsHealth = 100;
    [SerializeField] GameObject player;
    [SerializeField] int dead;

    Animator ani;
    playerAttack playAtk;
    float deathTimer;
    private void Start()
    {
        playAtk = player.GetComponent<playerAttack>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        if(racoonsHealth <= 0)
        {
            ani.SetBool("isDieing", true);
            deathTimer = Time.deltaTime;
            if(deathTimer >= dead)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            racoonsHealth -= playAtk.playerAtk;
        }
    }
}
