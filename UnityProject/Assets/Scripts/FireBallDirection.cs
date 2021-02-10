using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDirection : MonoBehaviour
{

    //public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public GameObject Fireball;
    FireballOrbit bossenemy;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Target");
        bossenemy = Fireball.GetComponent<FireballOrbit>();
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            bossenemy.MoveFireBall();
        }
        if (playerInTerritory == false)
        {
            //bossenemy.MoveToEnemy();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            
            playerInTerritory = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInTerritory = false;
        }
    }
}


