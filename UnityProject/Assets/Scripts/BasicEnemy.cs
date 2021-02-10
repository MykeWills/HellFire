using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicEnemy : MonoBehaviour
{
    public Transform target;
    public Transform normal;
   
    
    public float speed;
    public float falling = 3.0f;
    public int Health;

    public Camera targetCamera;
    public AudioSource monsterDeath;
    public AudioSource monsterHit;
    public GameObject Territory;
   
    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;
    public bool faceNormal;

    Rigidbody rb;

    public GameObject EnemyBlood;

    [SerializeField]
    private float fadePerSecond = 0.5f;

   
    // Use this for initialization
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        enemyFlashTimerStart = enemyFlashTimer;
        Health = 20;
        enemyFlashEnabled = false;
        faceTarget = true;
        faceNormal = false;
        EnableRagdoll();



    }

    // Update is called once per frame
    void Update()
    {

        if (faceTarget == true)
        {
            transform.LookAt(target);
            //transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
        }
        if (faceNormal == true)
        {
            transform.LookAt(normal);
        }

        if (Health <= 0){

            monsterDeath.Play();
    
            var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));

            Territory.SetActive(false);
            transform.Translate(Vector3.forward * falling * Time.deltaTime);
            enemyFlashEnabled = false;
            Destroy(gameObject, 0.5f);
        }

        if (enemyFlashEnabled == true)
        {
            EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;

        }
        else if (enemyFlashEnabled == false)
        {
            EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0)
        {

            EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;
        }
    }

    public void MoveToPlayer()
    {
        faceNormal = false;
        faceTarget = true;
        speed = 20;
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(-90, 0, 0), Space.Self);

    }
    public void MoveToNormal()
    {
        faceNormal = true;
        faceTarget = false;
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
        if (transform.position.y > -2)
        {
            faceTarget = true;
            faceNormal = false;
            speed = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            DisableRagdoll();
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 1;
            EnableRagdoll();
        }
        else if (other.gameObject.CompareTag("FlakBullet"))
        {
            DisableRagdoll();
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 2;
            EnableRagdoll();
        }
        else if (other.gameObject.CompareTag("PlasmaBullet"))
        {
            DisableRagdoll();
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 5;
            EnableRagdoll();
        }
        else if (other.gameObject.CompareTag("BFG"))
        {
            DisableRagdoll();
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 100;
            EnableRagdoll();
        }
        else if (other.gameObject.CompareTag("RailSlug"))
        {
            DisableRagdoll();
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 25;
            EnableRagdoll();
        }
        else if (other.gameObject.CompareTag("Rocket"))
        {
            DisableRagdoll();
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 50;
            EnableRagdoll();
        }
    }
    void EnableRagdoll()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
    void DisableRagdoll()
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }
}

