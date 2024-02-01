using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] bool predictiveShoot = true;
    [SerializeField] bool shootTowardsPlayer = false;
    [SerializeField] bool shootRandomly = true;
    [Header("connections")]
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject target;
    [SerializeField] AudioClip enemyShootSound;
    [Header("Bullet Settings")]
    [SerializeField] float bulletLifetime = 2;
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] float shootRange = 7f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float predictiveLead = 1;

    [Header("test")]
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 5f;
    [SerializeField] float shotsPerSecond = 2f;
    [SerializeField] bool clockwise = true;

    
    Animator myAnimator;
    float timer = 0f;
    float timer2 = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if(timer >= 1f / shotsPerSecond)
        {
            SpawnBullet();
            timer2 = 0f;
        }
        Vector3 playerPosition = target.transform.position;
        Vector3 shootDirection = playerPosition - transform.position;
        if (shootDirection.magnitude < shootRange && timer >= shootDelay && shootTowardsPlayer)
        {
            //Enemy predicts where to shoot
            if (predictiveShoot)
            {
                Vector3 playerVel = target.GetComponent<Rigidbody2D>().velocity;
                shootDirection += playerVel * predictiveLead;
            }
            timer = 0;
            myAnimator.Play("catusAttackAnimation", -1, 0f);
            shootDirection.Normalize();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyShootSound);
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            bullet.transform.up = shootDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
    void SpawnBullet()
    {
        float angle = timer2 * shotsPerSecond * 360f;
        if (!clockwise) angle = -angle;

        float radians = Mathf.Deg2Rad * angle;
        Vector3 offset = new Vector3(Mathf.Cos(radians) * radius, Mathf.Sin(radians) * radius);

        GameObject bullet = Instantiate(prefab, transform.position + offset, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = (clockwise ? Vector3.right : Vector3.left) * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}