using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int plHealth = 10;
    [SerializeField] int loadDelay = 1;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip tumbleweedCollision;
    [SerializeField] attackTumbleweed atkTums;
    [SerializeField] TextMeshProUGUI healthText;
    Animator playerAnimator;
    Rigidbody2D playerRB;
    PlayerMovement playerMove;

    void Start()
    {
        healthText.text = "Health: " + plHealth;
        playerAnimator = GetComponent<Animator>(); 
        playerRB = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTums.tumsTriggerAtk)
        {
            plHealth -= atkTums.atkTumbleweed;
            healthText.text = "Health: " + plHealth;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(tumbleweedCollision);
            if(plHealth <= 0)
            {
                playerMove.enabled = false;
                playerRB.gravityScale = 0;
                playerRB.velocity = Vector3.zero;
                playerAnimator.Play("DeathAnimation");
                Camera.main.GetComponent<AudioSource>().PlayOneShot(explosion);
                Invoke("ReloadScene", loadDelay);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTums.tumsCollisionAtk)
        {
            plHealth -= atkTums.atkTumbleweed;
            healthText.text = "Health: " + plHealth;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(tumbleweedCollision);
            if (plHealth <= 0)
            {
                playerMove.enabled = false;
                playerRB.gravityScale = 0;
                playerRB.velocity = Vector3.zero;
                playerAnimator.Play("DeathAnimation");
                Camera.main.GetComponent<AudioSource>().PlayOneShot(explosion);
                Invoke("ReloadScene", loadDelay);
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
