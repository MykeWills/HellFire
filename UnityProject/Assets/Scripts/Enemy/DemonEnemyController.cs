using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DemonEnemyController : MonoBehaviour
{
    [Header("Idle Movement")]
    Vector3 newPosition;
    public Transform BodyDemon;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Transform position4;
    public string State;
    public float smooth;
    public float resetTime;
    public int RandomNumber;
    public int RandomPosition;
    bool startIdle;
    public bool idle;
    public bool RandomizeMovement;
    public bool inRange;
    public bool Active;




    public float lastShot;
    public float fireRate;
    public float projectileForce;
    public Transform[] bulletSpawners;
    public GameObject EnemyProjectile;


    public float speed;

    Vector3 PlayerPosition;

    GameObject Player;

    public GameObject DemonBody;

    public float attackRange;
    public float stoppingDistance;
    public float startingDistance;
    public float awarenessRange;

    AudioSource audioSrc;
    public AudioClip Shoot;

    public float hoverHeight = 20.0f;
    public float hoverForce = 10.0F;
    public float hoverDamp = 0.5F;
    public Rigidbody rb;

    public GameObject MuzzleFlashObject;
    public float muzzleFlashTimer = 0.15f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public float MuzzleSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.5F;
        rb.angularDrag = 0.5F;
        muzzleFlashTimerStart = muzzleFlashTimer;
        inRange = false;
        startIdle = true;
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForPlayer());
        
        if (Vector3.Magnitude(transform.position - PlayerPosition) < attackRange && Time.time > lastShot + fireRate)
        {
            muzzleFlashEnabled = true;
            shoot();
        }
        if (Vector3.Magnitude(transform.position - PlayerPosition) < awarenessRange)
        {
            MoveToPlayer();
            Active = true;
            startIdle = true;
        }
        else
        {
            if (startIdle)
            {
                idle = true;
                MoveToIdle();
                startIdle = false;
            }
        }
        if (muzzleFlashEnabled == true)
        {
            MuzzleFlashObject.transform.Rotate(0, 90 * Time.deltaTime * MuzzleSpeed, 0);
            MuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            MuzzleFlashObject.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;
        }
    }
    void LateUpdate()
    {
        
        if (idle)
        {
            BodyDemon.position = Vector3.Lerp(BodyDemon.position, newPosition, smooth * Time.deltaTime);
        }
        else
        {
            if (Active)
            {
                var lookRot = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
                transform.rotation = Quaternion.LerpUnclamped(transform.rotation, lookRot, Time.deltaTime * 10f);
            }
        } 
    }
    public void FixedUpdate()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        Ray rightRay = new Ray(transform.position, Vector3.right);
        Ray leftRay = new Ray(transform.position, Vector3.left);

        if (Physics.Raycast(downRay, out hit))
        {
            float hoverError = hoverHeight - hit.distance;
            if (hoverError > 0)
            {
                float upwardSpeed = rb.velocity.y;
                float lift = hoverError * hoverForce - upwardSpeed * hoverDamp;
                rb.AddForce(lift * Vector3.up);
            }
        }
        if (Physics.Raycast(rightRay, out hit))
        {
            float hoverError = hoverHeight - hit.distance;
            if (hoverError > 0)
            {
                float rightwardSpeed = rb.velocity.x;
                float lift = hoverError * hoverForce - rightwardSpeed * hoverDamp;
                rb.AddForce(lift * Vector3.right);
            }
        }
        if (Physics.Raycast(leftRay, out hit))
        {
            float hoverError = hoverHeight - hit.distance;
            if (hoverError > 0)
            {
                float leftwardSpeed = rb.velocity.x;
                float lift = hoverError * hoverForce - leftwardSpeed * hoverDamp;
                rb.AddForce(lift * Vector3.left);
            }
        }
    }
    public void MoveToPlayer()
    {
        idle = false;

        if (Vector3.Magnitude(transform.position - PlayerPosition) > startingDistance)
        {
            inRange = false;
        }
        if (Vector3.Magnitude(transform.position - PlayerPosition) < stoppingDistance)
        {
            inRange = true;
            Active = false;
            
        }
        if (Vector3.Magnitude(transform.position - PlayerPosition) > stoppingDistance && !inRange)
        {
            Active = true;
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }
    public void MoveToIdle()
    {
        if (idle)
        {
            if (RandomizeMovement)
            {
                if (RandomPosition == 0)
                {
                    State = "Moving To Position 1";
                    RandomPosition = Random.Range(1, 4);
                    newPosition = position1.position;
                    transform.LookAt(position1.transform);
                }
                else if (RandomPosition == 1)
                {
                    RandomPosition = Random.Range(1, 4);
                    State = "Moving To Position 2";
                    newPosition = position2.position;
                    transform.LookAt(position2.transform);
                }
                else if (RandomPosition == 2)
                {
                    RandomPosition = Random.Range(1, 4);
                    State = "Moving To Position 3";
                    newPosition = position3.position;
                    transform.LookAt(position3.transform);
                }
                else if (RandomPosition == 3)
                {
                    RandomPosition = Random.Range(1, 4);
                    State = "Moving To Position 4";
                    newPosition = position4.position;
                    transform.LookAt(position4.transform);
                }
                else if (RandomPosition == 4)
                {
                    RandomPosition = Random.Range(1, 4);
                    State = "Moving To Position 1";
                    newPosition = position1.position;
                    transform.LookAt(position1.transform);
                }

                Invoke("MoveToIdle", resetTime);
            }
            else
            {
                if (RandomPosition == 0)
                {
                    RandomPosition = 1;
                    State = "Moving To Position 1";
                    newPosition = position1.position;
                    transform.LookAt(position1.transform);
                }
                else if (RandomPosition == 1)
                {
                    RandomPosition = 2;
                    State = "Moving To Position 2";
                    newPosition = position2.position;
                    transform.LookAt(position2.transform);
                }
                else if (RandomPosition == 2)
                {
                    RandomPosition = 3;
                    State = "Moving To Position 3";
                    newPosition = position3.position;
                    transform.LookAt(position3.transform);
                }
                else if (RandomPosition == 3)
                {
                    RandomPosition = 4;
                    State = "Moving To Position 4";
                    newPosition = position4.position;
                    transform.LookAt(position4.transform);
                }
                else if (RandomPosition == 4)
                {
                    RandomPosition = 1;
                    State = "Moving To Position 1";
                    newPosition = position1.position;
                    transform.LookAt(position1.transform);
                }
                Invoke("MoveToIdle", resetTime);
            }
        }
    }
    public void shoot()
    {

        for (int i = 0; i < bulletSpawners.Length; i++)
        {
            audioSrc.clip = Shoot;
            audioSrc.PlayOneShot(Shoot, 0.7f);
            Rigidbody Temporary_RigidBody;
            GameObject newBullet = Instantiate(EnemyProjectile, bulletSpawners[0].position, bulletSpawners[i].rotation) as GameObject;
            Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
            Destroy(newBullet, 10.0f);

        }
        lastShot = Time.time;
    }
    IEnumerator WaitForPlayer()
    {
        Player = GameObject.Find("/Player/LookAt/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/Player/LookAt/");
            PlayerPosition = Player.gameObject.transform.position;
        }
    }


    void OnDrawGizmosSelected()
    {  
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(DemonBody.transform.position, awarenessRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(DemonBody.transform.position, stoppingDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(DemonBody.transform.position, startingDistance);

    }

}
