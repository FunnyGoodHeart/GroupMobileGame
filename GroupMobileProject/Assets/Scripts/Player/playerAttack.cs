using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerAttack : MonoBehaviour
{
    [SerializeField] public int playerAtk= 1;
    [SerializeField] public bool plTriggerAtk = false;
    [SerializeField] public bool plCollisionAtk = true;
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] GameObject prefab;
    [SerializeField] float shootSpeed = 10;
    [SerializeField] float bulletLifetime = 2;
    [SerializeField] bool mouseShoot = true;
    [SerializeField] int bulletCount = 60;
    [SerializeField] bool playerShoot = true;
    [SerializeField] AudioClip shootingSound;
    int maxBulletCount = 120;
    float x = 2;
    float y = 0;

    Animator myAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Picks up Max Ammo 
        if (collision.gameObject.tag == "Max Ammo" && bulletCount < maxBulletCount)
        {
            bulletCount = 200;
            if (bulletCount > maxBulletCount)
            {
                bulletCount = maxBulletCount;
            }
            myText.text = "Ammo: " + bulletCount;
            Destroy(collision.gameObject);
        }
        // Picks up regular ammo
        else if (collision.gameObject.tag == "Ammo Crate" && bulletCount < maxBulletCount)
        {
            bulletCount += 6;
            if (bulletCount > maxBulletCount)
            {
                bulletCount = maxBulletCount;
            }
            myText.text = "Ammo: " + bulletCount;
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = Input.GetAxisRaw("Horizontal");
        float tempY = Input.GetAxisRaw("Vertical");

        if (bulletCount <= 0)
        {
            playerShoot = false;
        }

        else if (bulletCount > 0)
        {
            playerShoot = true;
        }

        if (mouseShoot)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 shootDir = mousePosition - transform.position;
            shootDir.z = 0;
            shootDir.Normalize();
            x = shootDir.x;
            y = shootDir.y;
        }
        else if (tempX != 0 || tempY != 0)
        {
            x = tempX;
            y = tempX;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // I have pressed the fire button
            if (playerShoot)
            {
                bulletCount--;
                myText.text = "Ammo: " + bulletCount;
                Camera.main.GetComponent<AudioSource>().PlayOneShot(shootingSound);
                GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * shootSpeed;
                Destroy(bullet, bulletLifetime);
            }
        }
    }
}
