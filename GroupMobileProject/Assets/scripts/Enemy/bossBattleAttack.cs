using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBattleAttack : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject Target;
    [SerializeField] GameObject Hand;

    [Header("racoon's Attack")]
    [SerializeField] public int swipeAtk = 1;
    [SerializeField] public int shootAtk = 1;
    [SerializeField] int swipeTime = 1;
    [SerializeField] int ammountOfHandColliders = 11;

    [Header("random Stuff")]
    [SerializeField] int minShootNum = 1;
    [SerializeField] int maxShootNum = 5;
    [SerializeField] int minSwipeNum = 1;
    [SerializeField] int maxSwipeNum = 5;
    [SerializeField] int coolDownAtkTime = 2;

    [Header("testing animations")]
    [SerializeField] bool shootNow = false;
    [SerializeField] bool swipeNow = false;

    [Header("timers")]
    [SerializeField] float colliderTimer;
    [SerializeField] float shootTimer;
    [SerializeField] float swipeTimer;
    [SerializeField] float cooldown; // puts attacks on cooldown

    int randomShootInterval;
    int randomSwipeInterval;
    bool swipping = false;
    bool onCooldown = false;
    bool firstSwing = true;
    bool cooldownStart = true;
    PolygonCollider2D handcollision;
    SpriteRenderer handsprite;
    Animator ani;
    Animator handAni;
    [SerializeField] int randomAttacks;
    // Start is called before the first frame update
    void Start()
    {

        handcollision = Hand.GetComponent<PolygonCollider2D>();
        ani = GetComponent<Animator>();
        maxShootNum += 1;   maxSwipeNum += 1;   //allows it to be the number that was put into the serilize field;
        randomShootInterval = Random.Range(minShootNum, maxShootNum);
        randomSwipeInterval = Random.Range(minSwipeNum, maxSwipeNum);
        randomAttacks = Random.Range(1, 3);
        handAni = Hand.GetComponent<Animator>();
        handsprite = Hand.GetComponent<SpriteRenderer>();
        handsprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        shootTimer += Time.deltaTime; swipeTimer += Time.deltaTime;
        cooldown += Time.deltaTime;
        if (shootTimer >= randomShootInterval && randomAttacks == 1 || shootNow )
        {
            Debug.Log("pew pew");
            ani.SetTrigger("isShooting");
            onCooldown = true;
            shootTimer = 0;
            shootNow = false;
            onCooldown = true;
        }
        else if (swipeTimer >= randomSwipeInterval && randomAttacks == 2 || swipeNow)
        {
            Debug.Log("swipp");
            RacoonSwipe();
            onCooldown = true;
        }
        if (onCooldown!)
        {
            Debug.Log("empty cooldown");
            cooldown = 0;
        }
        else if (onCooldown)
        {
            Debug.Log("brr cold for cool down");
            if (cooldownStart)
            {
                Debug.Log("cooldown start");
                cooldown = 0;
                randomAttacks = Random.Range(1, 3);
                cooldownStart = false;
            }
            if (cooldown >= coolDownAtkTime)
            {
                Debug.Log("cooldown end");
                cooldown = 0;
                shootTimer = 0; swipeTimer = 0;
                cooldownStart = true;
                onCooldown = false;
            }
        }
    }
    void RacoonSwipe()
    {
        handsprite.enabled = true;
        swipping = true;
        ani.SetTrigger("isSwiping");
        handAni.SetTrigger("isSwipping");
        swipeTimer = 0;
        swipeNow = false;

    }
}
