using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int plHealth = 10;
    [SerializeField] int loadDelay = 1;
    [SerializeField] attackTumbleweed atkTums;
    [SerializeField] TextMeshProUGUI healthText;
    Animator playerAnimator;
    Rigidbody2D playerRB;

    void Start()
    {
        healthText.text = "Health: " + plHealth;
        playerAnimator = GetComponent<Animator>(); 
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTums.tumsTriggerAtk)
        {
            plHealth -= atkTums.atkTumbleweed;
            healthText.text = "Health: " + plHealth;
            if(plHealth <= 0)
            {
                playerAnimator.SetTrigger("isDead");
                Invoke("ReloadScene", loadDelay);
            }
        }

        /*if (collision.gameObject.tag == "Enemy Bullet")
        {
            plHealth -= another.script;
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTums.tumsCollisionAtk)
        {
            plHealth -= atkTums.atkTumbleweed;
            healthText.text = "Health: " + plHealth;
            if (plHealth <= 0)
            {
                playerAnimator.SetTrigger("isDead");
                Invoke("ReloadScene", loadDelay);
            }
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
