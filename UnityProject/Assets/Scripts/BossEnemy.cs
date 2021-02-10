using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossEnemy : MonoBehaviour
{
    
    public BoxCollider BossTerritory;
    public BoxCollider FireballTerritory;
    public Transform target;
    public Transform center;
    public Text BossHealthText;
    public CapsuleCollider PistolBullet;
    public CapsuleCollider AssaultRifleBullet;
    public MeshCollider FlakCannonBullet;
    public MeshCollider PlasmaRifleBullet;
    public SphereCollider BFGProtonBullet;
    public BoxCollider RailGunBullet;
    public MeshCollider TurnOffBoss;
    public float degreesPerSecond = -65.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    public int Health = 50;
    public float falling = 3.0f;
    public Camera targetCamera;
    public AudioSource monsterDeath;
    public AudioSource monsterHit;
    public AudioSource BossMusic;
    public AudioSource levelMusic;
    public GameObject Territory;
    public GameObject Boss;
    public GameObject BossTrigger;
    public GameObject Fireball;
    public GameObject PortalFinished;
    private Vector3 distance;
    public GameObject EnemyBlood;
    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled = false;
    [SerializeField]
    private float fadePerSecond = 2.5f;

    public float shakeDuration;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public AudioSource QuakeSound;
    public AudioSource ThunderSound;
    public Transform camTransform;
    Vector3 originalPos;



    // Use this for initialization
    void Start()
    {

        enemyFlashTimerStart = enemyFlashTimer;
        PistolBullet.isTrigger = false;
        AssaultRifleBullet.isTrigger = false;
        FlakCannonBullet.isTrigger = false;
        PlasmaRifleBullet.isTrigger = false;
        BFGProtonBullet.isTrigger = false;
        RailGunBullet.isTrigger = false;
        PortalFinished.SetActive(false);
        distance = transform.position - center.position;
        distance.x = DistanceX;
        distance.y = DistanceY;
        distance.z = DistanceZ;
        

    }
    // Update is called once per frame
    void Update()
    {

        
        if (Health < 0)
        {
            
            enemyFlashEnabled = false;
            // BossMovementScript.enabled = false;
            var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            transform.Translate(Vector3.forward * falling * Time.deltaTime);
           
            TurnOffBoss.isTrigger = false;
            Territory.SetActive(false);
            Fireball.SetActive(false);
            EnemyBlood.SetActive(false);
            BossTrigger.SetActive(false);
            //gameObject.SetActive(false);
            PortalFinished.SetActive(true);
            BossMusic.Stop();
            BossHealthText.text = "";
            
            Destroy(BossTerritory);
            Destroy(FireballTerritory);
        }
        if (enemyFlashEnabled == true)
        {
            EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;
        }
        if (enemyFlashTimer <= 0)
        {
            EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;
        }
        if (shakeDuration > 0)
        {
            
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else if (shakeDuration < 0)
        {
            QuakeSound.Stop();
            levelMusic.Play();
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
    public void MeshRotation()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
        transform.position = center.position + distance;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 1;
            SetCountHealth();
            if (Health  <= 0)
            {
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                shakeDuration += 3;
            }
        }
        else if (other.gameObject.CompareTag("FlakBullet"))
        { 
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 2;
            SetCountHealth();
            if (Health <= 0)
            {
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                shakeDuration += 3;
            }
        }
        else if (other.gameObject.CompareTag("PlasmaBullet"))
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 5;
            SetCountHealth();
            if (Health <= 0)
            {
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                shakeDuration += 3;
            }
        }
        else if (other.gameObject.CompareTag("BFG"))
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 100;
            SetCountHealth();
            if (Health <= 0)
            {
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                shakeDuration += 3;
            }
        }
        else if (other.gameObject.CompareTag("RailSlug"))
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 25;
            SetCountHealth();
            if (Health <= 0)
            {
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                shakeDuration += 3;
            }
        }
    }
    void SetCountHealth()
    {
        BossHealthText.text = "Mega CacoDemon " + Health.ToString();
    }
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }
}

