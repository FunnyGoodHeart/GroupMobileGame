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
    [Header("random Stuff")]
    [SerializeField] int minShootNum = 1;
    [SerializeField] int maxShootNum = 5;
    [SerializeField] int minSwipeNum = 1;
    [SerializeField] int maxSwipeNum = 5;
    [SerializeField] int coolDownAtkTime = 2;
    [Header("testing animations")]
    [SerializeField] bool shootNow = false;
    [SerializeField] bool swipeNow = false;


    int randomShootInterval;
    int randomSwipeInterval;
    float shootTimer;
    float swipeTimer;
    float cooldown; // puts attacks on cooldown
    bool onCooldown;
    Animator ani;
    Animator handAni;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        maxShootNum += 1;   maxSwipeNum += 1;   //allows it to be the number that was put into the serilize field;
        randomShootInterval = Random.Range(minShootNum, maxShootNum);
        randomSwipeInterval = Random.Range(minSwipeNum, maxSwipeNum);
        handAni = Hand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        else if (shootTimer >= randomShootInterval || shootNow)
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
}
