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
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject tumbleweedParet; //connect the parent that is holding the enemy type
    [SerializeField] GameObject catusParent; //same here

    Animator playerAnimator;
    Rigidbody2D playerRB;
    PlayerMovement playerMove;
    bool collsionAttack;
    GameObject hit;
    attackTumbleweed atkTums;
    attackTumbleweed atkCatus;

    void Start()
    {
        healthText.text = "Health: " + plHealth;
        playerAnimator = GetComponent<Animator>(); 
        playerRB = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMovement>();
        atkTums = tumbleweedParet.GetComponent<attackTumbleweed>();
        atkCatus = catusParent.GetComponent<attackTumbleweed>();
    }
    private void Update()
    {
        collsionAttack = atkTums.enemyCollision;
        if (plHealth <= 0)
        {
            playerMove.enabled = false;
            playerRB.gravityScale = 0;
            playerAnimator.Play("DeathAnimation");
            Camera.main.GetComponent<AudioSource>().PlayOneShot(explosion);
            Invoke("ReloadScene", loadDelay);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TumbleWeed" && collsionAttack)
        {
            plHealth -= atkTums.enemyAtk;
            healthText.text = "Health: " + plHealth;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(tumbleweedCollision);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "catus" && atkCatus.enemyTrigger || collision.gameObject.tag == "Enemy Bullet" && atkCatus.enemyTrigger)
        {
            plHealth -= atkCatus.enemyAtk;
            healthText.text = "Health: " + plHealth;
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
