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
    [SerializeField] float colliderTimer;

    public GameObject[] handObjects;

    int handObjectIndex = 0;
    int randomShootInterval;
    int randomSwipeInterval;
    float shootTimer;
    float swipeTimer;
    float cooldown; // puts attacks on cooldown
    bool swipping = false;
    bool onCooldown;
    PolygonCollider2D handcollision;
    Animator ani;
    Animator handAni;
    // Start is called before the first frame update
    void Start()
    {
        handcollision = Hand.GetComponent<PolygonCollider2D>();
        ani = GetComponent<Animator>();
        maxShootNum += 1;   maxSwipeNum += 1;   //allows it to be the number that was put into the serilize field;
        randomShootInterval = Random.Range(minShootNum, maxShootNum);
        randomSwipeInterval = Random.Range(minSwipeNum, maxSwipeNum);
        handAni = Hand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RacoonSwipe();
        if (onCooldown)
        {
            cooldown += Time.deltaTime;
            if(cooldown >= coolDownAtkTime)
            {
                shootTimer = 0;     swipeTimer = 0;
                onCooldown = false;
            }
        }
        if (onCooldown!)
        {
            shootTimer += Time.deltaTime; swipeTimer += Time.deltaTime;
        }
        if (shootTimer >= randomShootInterval || shootNow)
        {
            ani.SetTrigger("isShooting");
            onCooldown = true;
            shootTimer = 0;
            shootNow = false;
        }
        else if (swipeTimer >= randomSwipeInterval || swipeNow)
        {
            
            ani.SetTrigger("isSwiping");
            handAni.SetTrigger("isSwipping");
            swipeTimer = 0;
            swipeNow = false;
        }
    }
    void RacoonSwipe()
    {
        //this is gonna be a little funky area to make sure the collision keeps up with the boss
        float betweenAniTimes = swipeTime / ammountOfHandColliders;
        if (swipping)
        {
            colliderTimer += Time.deltaTime;
        }
        else
        {
            colliderTimer = 0;
        }
        if(colliderTimer >= swipeTime)
        {
            PolygonCollider2D currentHand;
            handObjectIndex = (handObjectIndex + 1) % handObjects.Length;
            currentHand = handObjects[handObjectIndex].GetComponent<PolygonCollider2D>();
            currentHand.enabled = true;
        }
    }
}
