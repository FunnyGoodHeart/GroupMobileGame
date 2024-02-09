using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTumbleweed : MonoBehaviour
{
    //change it where it can be changed between enemie groups
    //put all the same enemies in a parent then
    //put this script in parent then connect to player
    [SerializeField] public int enemyAtk = 5;
    //if the enemy attacks either with collision or trigger
    [SerializeField] public bool enemyCollision;
    [SerializeField] public bool enemyTrigger;
}