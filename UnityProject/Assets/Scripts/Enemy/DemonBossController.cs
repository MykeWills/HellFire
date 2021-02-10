using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DemonBossController : MonoBehaviour
{
    public Collider BossCollision;
    public AudioSource LevelMusic;
    public AudioSource BossMusic;
    AudioSource audioSrc;
    public AudioClip EnemyHit;
    public AudioClip Shoot;

    Vector3 PlayerPosition;
    GameObject Player;

    public float awarenessRange;

    public float lastShot;
    public float fireRate;
    public float projectileForce;
    public GameObject EnemyProjectile;

    public Transform[] bulletSpawners;

    bool StartMusic;

    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    private Vector3 distance;
    public float degreesPerSecond = -65.0f;

    public Transform Center;
    public bool StartOrbit;

    public GameObject MuzzleFlashObject;
    public float muzzleFlashTimer = 0.15f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public float MuzzleSpeed;

    // Use this for initialization
    void Start()
    {
        StartMusic = true;
        audioSrc = GetComponent<AudioSource>();
        distance = transform.position - Center.position;
        muzzleFlashTimerStart = muzzleFlashTimer;
        distance.x = DistanceX;
        distance.y = DistanceY;
        distance.z = DistanceZ;
        BossCollision.enabled = false; 
    }
    void Update()
    {
        StartCoroutine(WaitForPlayer());

        if (Vector3.Magnitude(transform.position - PlayerPosition) <= awarenessRange &&!StartOrbit)
        {
            StartOrbit = true;
            
            if (StartMusic)
            {
                LevelMusic.Stop();
                BossMusic.Play();
                BossCollision.enabled = true;
                StartMusic = false;
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
    public void LateUpdate()
    {
        if (StartOrbit)
        {
            
           
            StartMovement();
            if (Time.time > lastShot + fireRate)
            {
                muzzleFlashEnabled = true;
                shoot();
            }
        }
    }
    IEnumerator WaitForPlayer()
    {
        Player = GameObject.Find("/Player/");

        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/Player/");
            PlayerPosition = Player.gameObject.transform.position;

        }
    }

    public void StartMovement()
    {
        var lookRot = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation, lookRot, Time.deltaTime * 10f);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
        transform.position = Center.position + distance;
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, awarenessRange);
    }
}
