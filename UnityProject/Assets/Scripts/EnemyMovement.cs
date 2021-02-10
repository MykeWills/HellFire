using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public AudioSource monsterEncounter;
    public GameObject enemy;
    BasicEnemy basicenemy;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Target");
        basicenemy = enemy.GetComponent<BasicEnemy>();
        playerInTerritory = false;
        //enemy.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            //enemy.SetActive(true);
            basicenemy.MoveToPlayer();
        }
        if (playerInTerritory == false)
        {
            basicenemy.MoveToNormal();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            monsterEncounter.Play();
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


