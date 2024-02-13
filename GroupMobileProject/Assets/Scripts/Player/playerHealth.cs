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
    [SerializeField] GameObject tumbleweedParent; //connect parent game object to these
    [SerializeField] GameObject catusParent;//same here
    Animator playerAnimator;
    Rigidbody2D playerRB;
    PlayerMovement playerMove;
    attackTumbleweed atkTumble;
    attackTumbleweed atkCatus;


    void Start()
    {
        healthText.text = "Health: " + plHealth;
        playerAnimator = GetComponent<Animator>(); 
        playerRB = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMovement>();
        atkTumble = tumbleweedParent.GetComponent<attackTumbleweed>();
        atkCatus = catusParent.GetComponent<attackTumbleweed>();

    }
    private void Update()
    {
        if (plHealth <= 0)
        {
            isDying();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Catus" && atkCatus.enemyTrigger || collision.gameObject.tag == "Enemy Bullet" && atkCatus.enemyTrigger )
        {
            plHealth -= atkTumble.enemyAtk;
            healthText.text = "Health: " + plHealth;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(tumbleweedCollision);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && atkTumble.enemyCollision)
        {
            plHealth -= atkTumble.enemyAtk;
            healthText.text = "Health: " + plHealth;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(tumbleweedCollision);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void isDying()
    {
        playerMove.enabled = false;
        playerRB.gravityScale = 0;
        playerRB.velocity = Vector3.zero;
        playerAnimator.Play("DeathAnimation");
        Camera.main.GetComponent<AudioSource>().PlayOneShot(explosion);
        Invoke("ReloadScene", loadDelay);
    }
}
