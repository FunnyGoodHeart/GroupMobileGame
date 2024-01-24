using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTumbleweed : MonoBehaviour
{
    [SerializeField] public int atkTumbleweed = 5;
    [SerializeField] public bool tumsTriggerAtk = false;
    [SerializeField] public bool tumsCollisionAtk = true;
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] float shootRange = 7f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject target;
    [SerializeField] float bulletLifetime = 2;
    float timer = 0;
    [SerializeField] bool predictiveShoot = true;
    [SerializeField] float predictiveLead = 1;
    [SerializeField] AudioClip enemyShootSound;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 playerPosition = target.transform.position;
        Vector3 shootDirection = playerPosition - transform.position;
        if (shootDirection.magnitude < shootRange && timer >= shootDelay)
        {
            if (predictiveShoot)
            {
                Vector3 playerVel = target.GetComponent<Rigidbody2D>().velocity;
                shootDirection += playerVel * predictiveLead;
            }
            timer = 0;
            //myAnimator.SetTrigger("isShooting");
            shootDirection.Normalize();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyShootSound);
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            bullet.transform.up = shootDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}