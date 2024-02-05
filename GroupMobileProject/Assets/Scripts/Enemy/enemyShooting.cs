using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    [Header("enemyTypes")]
    [SerializeField] bool isCatus = false;
    [Header("Setup")]
    [SerializeField] bool predictiveShoot = true;
    [SerializeField] bool shootTowardsPlayer = false;
    [SerializeField] bool shootRandomly = true;
    [Header("connections")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject target;
    [SerializeField] Transform targetTransform;
    [SerializeField] AudioClip enemyShootSound;
    [Header("Bullet Settings")]
    [SerializeField] float bulletLifetime = 2;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float shootRange = 7f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float predictiveLead = 1;
    [SerializeField] int bulletAmmount = 10;
    [Header("test")]
    int numBullets;
    float nextFireTime;
    
    //ect
    Animator myAnimator;
    float timer = 0f;
    float timer2 = 0f;
    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        Vector3 playerPosition = target.transform.position;
        Vector3 shootDirection = playerPosition - transform.position;
        if (shootDirection.magnitude < shootRange && timer >= fireRate && shootTowardsPlayer)
        {
            //Enemy predicts where to shoot
            if (predictiveShoot)
            {
                Vector3 playerVel = target.GetComponent<Rigidbody2D>().velocity;
                shootDirection += playerVel * predictiveLead;
            }
            else if(Time.time > nextFireTime && shootRandomly)
            {
                ShootBullets();
                nextFireTime = Time.time + fireRate;
            }
            timer = 0;
            if (isCatus)
            {
                myAnimator.Play("catusAttackAnimation", -1, 0f);
            }
            shootDirection.Normalize();
            //Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyShootSound);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.up = shootDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }

    void ShootBullets ()
    {
        float angleStep = 360f / numBullets;
        float currentAngle = transform.eulerAngles.z;
        for (int i = 0; i < numBullets; i++)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}