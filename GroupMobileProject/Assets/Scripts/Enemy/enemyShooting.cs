using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    [Header("enemyTypes")]      //what kind of charcter is it? (this is for animations)
    [SerializeField] bool isCatus = false;
    [Header("Setup")]       //what kind of shoot setup for the enemy?
    [SerializeField] bool predictiveShoot = true;
    [SerializeField] bool shootTowardsPlayer = false;
    [SerializeField] bool shootRandomly = true;
    [Header("connections")]     //prefabs and the player target
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject target;
    [SerializeField] Transform targetTransform;
    [SerializeField] AudioClip enemyShootSound;
    [Header("Bullet Settings")]     //some are for only random shoot/target player and some other are for both
    [SerializeField] float fireRate = 4f;
    [SerializeField] float shootRange = 7f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float predictiveLead = 1;
    [SerializeField] float rotationspeed = 180f;    
    [SerializeField] int bulletsPerRotation = 8;

    //bullet lifetime is in the bullet prefab (script called BulletDestroy)

    //ect
    float nextFireTime;
    float currentAngle;
    Animator myAnimator;
    float timer = 0f;
    float timer2 = 0f;
    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        
        timer2 += Time.deltaTime;
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
            timer = 0;
            if (isCatus)
            {
                Debug.Log("player towards shoot");
                myAnimator.SetTrigger("isShooting");
            }
            shootDirection.Normalize();
            //Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyShootSound);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.up = shootDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        }
        else if (Time.time >= nextFireTime && shootRandomly)
        {
            Debug.Log("step 1");
            currentAngle = 0f;
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet ()
    {
        Debug.Log("Step 2");
        if (isCatus)
        {
            Debug.Log("antmation");
            myAnimator.Play("catusAttackAnimation", -1, 0f);
        }
        for (int i = 0; i < bulletsPerRotation; i++)
        {
            Debug.Log("Fire in the hole");
            float angleRadians = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
            GameObject projectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            currentAngle += 360f / bulletsPerRotation;
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