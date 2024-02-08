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
    [Header("test")]
    int numBullets;
    float nextFireTime = 3;
    
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
                Debug.Log("what?");
                myAnimator.Play("catusAttackAnimation", -1, 0f);
            }
            shootDirection.Normalize();
            //Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyShootSound);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.up = shootDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
        else if (timer2 > nextFireTime && shootRandomly)
        {
            Debug.Log("step1");
            ShootBullets();
            timer2 = 0;
        }
    }

    void ShootBullets ()
    {
        Debug.Log("step2");
        timer2 = 0;
        float angleStep = 360f / numBullets;
        float currentAngle = transform.eulerAngles.z;
        if (isCatus)
        {
            Debug.Log("animation");
            myAnimator.Play("catusAttackAnimation", -1, 0f);
        }
        //for (int i =0; i >= numBullets; i++)
        //{
            Debug.Log("step3");
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * Vector2.up;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            currentAngle += angleStep;
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}