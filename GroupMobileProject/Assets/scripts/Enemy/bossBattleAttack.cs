using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBattleAttack : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject Hand;
    [SerializeField] GameObject HandBox;
    [SerializeField] GameObject rabiesbullet;

    [Header("rabies")]
    [SerializeField] float shootRange = 7f;
    [SerializeField] bool predictiveShoot = true;
    [SerializeField] float predictiveLead = 1;
    [SerializeField] AudioClip enemyShootSound;
    [SerializeField] float bulletSpeed = 2f;

    [Header("racoon's Attack")]
    [SerializeField] public int swipeAtk = 1;
    [SerializeField] public int shootAtk = 1;

    [Header("random Stuff")]
    [SerializeField] int minCoolDownTime = 4;
    [SerializeField] int maxCoolDownTime = 6;
    [SerializeField] int timeOfSwipe = 2;

    [Header("Bool")]
    [SerializeField] bool shootNow = false;
    [SerializeField] bool swipeNow = false;

    [Header("timers")]
    [SerializeField] float colliderTimer;
    [SerializeField] float cooldown; // puts attacks on cooldown
    float timer;

    bool onCooldown = true;
    bool cooldownStart = true;
    bool clearBox = false;
    BoxCollider2D handHitBox;
    SpriteRenderer handsprite;
    Animator ani;
    Animator handAni;
    int randomAttacks;
    int randomCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        randomCoolDown = Random.Range(minCoolDownTime, maxCoolDownTime);
        handHitBox = HandBox.GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        randomAttacks = Random.Range(1, 3);
        handAni = Hand.GetComponent<Animator>();
        handsprite = Hand.GetComponent<SpriteRenderer>();
        handsprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        colliderTimer += Time.deltaTime;
        cooldown += Time.deltaTime;
        if (randomAttacks == 1 && onCooldown == false || shootNow) 
        {
            RacoonShoot();
        }
        else if ( randomAttacks == 2 && onCooldown == false || swipeNow) 
        {
            RacoonSwipe();
            onCooldown = true;
        }
        DoCoolDown();
    }
    void DoCoolDown()
    {
        if (colliderTimer > timeOfSwipe && clearBox == true)
        {
            colliderTimer = 0;
            handHitBox.enabled = false;
            clearBox = false;
        }
        if (onCooldown == false && clearBox == false)
        {
            cooldown = 0;
        }
        if (onCooldown == true && clearBox==false)
        {
            if (cooldownStart)
            {
                cooldown = 0;
                colliderTimer = 0;
                randomAttacks = Random.Range(1, 3);
                cooldownStart = false;
            }
            if (cooldown >= randomCoolDown)
            {
                cooldown = 0;
                colliderTimer = 0;
                cooldownStart = true;
                onCooldown = false;
                randomCoolDown = Random.Range(minCoolDownTime, maxCoolDownTime);
            }
        }
    }
    void RacoonShoot()
    {
        ani.SetTrigger("isShooting");
        onCooldown = true;
        shootNow = false;

        Vector3 playerPosition = target.transform.position;
        Vector3 shootDirection = playerPosition - transform.position;
        if (shootDirection.magnitude < shootRange)
        {
            //Enemy predicts where to shoot
            if (predictiveShoot)
            {
                Vector3 playerVel = target.GetComponent<Rigidbody2D>().velocity;
                shootDirection += playerVel * predictiveLead;
            }
            shootDirection.Normalize();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyShootSound);
            GameObject bullet = Instantiate(rabiesbullet, transform.position, Quaternion.identity);
            bullet.transform.up = shootDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        }
    }
    void RacoonSwipe()
    {
        handHitBox.enabled = true;
        handsprite.enabled = true;
        ani.SetTrigger("isSwiping");
        handAni.SetTrigger("isSwipping");
        swipeNow = false;
        clearBox = true;
    }
}
