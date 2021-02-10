using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

  
    public MeshCollider TurnOffBoss;
    GameObject player;
    bool playerInTerritory;
    public AudioSource monsterEncounter;
    public AudioSource LevelMusic;
    public AudioSource BossMusic;
    public GameObject triggerBoss;
    public GameObject Boss;
    public GameObject BossTriggerMesh;
    public GameObject FireballTrigger;
    BoxCollider TerritoryBox;
    BossEnemy bossenemy;
    BossRotation bossMovement;

    // Use this for initialization
    void Start()
    {
        TerritoryBox = gameObject.GetComponent<BoxCollider>();
        playerInTerritory = false;
        player = GameObject.FindGameObjectWithTag("Target");
        // Set boss Mesh off and fireball off
        TurnOffBoss.isTrigger = false;
        BossTriggerMesh.SetActive(false);
        FireballTrigger.SetActive(false);
        // rotate both mesh and boss gameobject
        bossenemy = triggerBoss.GetComponent<BossEnemy>();
        bossMovement = Boss.GetComponent<BossRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            // Star boss Mesh  and fireball start shooting
            //BossTriggerMesh.SetActive(true);
            FireballTrigger.SetActive(true);
            // start rotating both mesh and boss gameobject
            bossenemy.MeshRotation();
            bossMovement.RotateAroundPlayer();
            TerritoryBox.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //When player enters the trigger box
            LevelMusic.Stop();
            BossMusic.Play();
            monsterEncounter.Play();
            playerInTerritory = true;
            TurnOffBoss.isTrigger = true;
            
        }
    }

}


