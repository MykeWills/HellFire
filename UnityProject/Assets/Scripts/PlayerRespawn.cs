using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerRespawn : MonoBehaviour
{
    public float shakeAmount = 0.7f;
    float decreaseFactor = 1.0f;
    public Transform camTransform;
    Vector3 originalPos;
    GameObject CheckpointBox;
    public int lives;
    public static float health = 100;
    public int Assault;
    public int flak;
    public int plasma;
    public int bfgproton;
    public int rail;
    public int rocket;

    public Text countLives;
    public Text countHealth;
    public Text PickupText;
    public bool BossDead;
    public bool GameEnd;

    public GameObject PistolcountAmmo;
    public GameObject AssaultcountAmmo;
    public GameObject FlakcountAmmo;
    public GameObject PlasmacountAmmo;
    public GameObject BFGcountAmmo;
    public GameObject RailcountAmmo;
    public GameObject RocketcountAmmo;
    public GameObject PistolGun;
    public GameObject AssaultRifleGun;
    public GameObject FlakCannonGun;
    public GameObject PlasmaRifleGun;
    public GameObject BFGProtonGun;
    public GameObject railGunGun;
    public GameObject rocketLauncherGun;
    public GameObject redScreen;
    public GameObject whiteScreen;
    public GameObject WarpPortal;

    public bool AmmoShot;
    public bool Pistol;
    public bool AssaultRifle;
    public bool FlakCannon;
    public bool PlasmaRifle;
    public bool BFG;
    public bool RailGun;
    public bool RocketLauncher;
    public bool redScreenFlashEnabled = false;
    public bool whiteScreenFlashEnabled = false;

    public AudioSource levelMusic;
    public AudioSource BossMusic;
    public AudioSource deathSound;
    public AudioSource ammoSound;
    public AudioSource healthSound;
    public AudioSource portal;
    public AudioSource BossDied;


    public float redScreenFlashTimer = 0.1f;
    public float whiteScreenFlashTimer = 0.1f;
    private float redScreenFlashTimerStart;
    private float whiteScreenFlashTimerStart;
    public float FadeTime;
    public float FadeSpeed;
    public float DamageSpeed = 2.0f;
    public bool Lava;

    public float quakeTimer;
    public bool BossFireBallDam;
    private AssaultRifle AssaultRifleScript;
    private FlakCannon FlakCannonScript;
    private PlasmaRifle PlasmaRifleScript;
    private BFGProton BFGProtonScript;
    private Pistol PistolScript;
    private RailGun RailGunScript;
    private RocketLauncher RocketLauncherScript;

    public float hurtTimer;
    float resetHurtTimer;
    public bool hurtPlayer = false;
    public bool EnemyFireBall = false;
    public  bool FireBall = false;
    public bool Enemy;
    public Vector3 LastCheckPoint;

    void Start()
    {
        resetHurtTimer = hurtTimer;
        CheckpointBox = GameObject.Find("/Player/LookAt/");
        WarpPortal.SetActive(false);
        GameEnd = false;
        AmmoShot = true;
        originalPos = camTransform.localPosition;
        lives = 9;
        health = 100;
        SetCountHealth();
        SetCountLives();
        BossFireBallDam = false;
        PickupText.text = "";
       

        // Set Timer variables and Speed
        redScreenFlashTimerStart = redScreenFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        FadeTime = 0.0f;
        FadeSpeed = 1.0f;
        Lava = false;
        

        // Set all guns to be off or on
        Assault = 1;
        flak = 1;
        plasma = 1;
        rail = 1;
        rocket = 1;
        bfgproton = 1;

        LastCheckPoint = CheckpointBox.transform.position;

        // Access all weapon scripts
        PistolScript = GetComponent<Pistol>();
        AssaultRifleScript = GetComponent<AssaultRifle>();
        FlakCannonScript = GetComponent<FlakCannon>();
        PlasmaRifleScript = GetComponent<PlasmaRifle>();
        BFGProtonScript = GetComponent<BFGProton>();
        RailGunScript = GetComponent<RailGun>();
        RocketLauncherScript = GetComponent<RocketLauncher>();

        // Set all weapon scripts to true or false
        ShutOffAllWeapons();
        SetupPistol();
    }

    void Update()
    {
        BossDead = DemonBossCollision.BossDead;
        if (hurtTimer > 0 && hurtPlayer)
        {
            hurtTimer -= Time.deltaTime;
        }
        if (hurtTimer < 0)
        {
            if (Enemy)
            {
                DoDamage(Random.Range(1, 5));
            }
            if (EnemyFireBall)
            {
                DoDamage(10);
            }
            if (FireBall)
            {
                DoDamage(20);
            }
            hurtTimer = resetHurtTimer;
            hurtPlayer = false;
            EnemyFireBall = false;
            FireBall = false;
            Enemy = false;

        }
        if (BossDead && !GameEnd)
        {
            quakeTimer += 5;
            levelMusic.Play();
            BossMusic.Stop();
            BossDied.Play();
            WarpPortal.SetActive(true);
            GameEnd = true;
        }

        if (quakeTimer > 0)
        {
            quakeTimer -= Time.deltaTime;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            

        }
        if (quakeTimer < 0)
        {
            quakeTimer = 0;
            camTransform.localPosition = originalPos;
        }
        
        // Set Fading timer for text to disappear 
        if (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }
        

        else if (FadeTime < 0)
        {
            FadeTime = 0;
            PickupText.text = "";
        }

        if (FadeTime > 5)
        {
            FadeTime = 5;
        }

        // Set Player Damage Timer if taking damage
       
        if (Lava)
        {
            DoDamage(1 * Time.deltaTime * DamageSpeed);
        }
        if (BossFireBallDam)
        {
            DoDamage(1 * Time.deltaTime * 20);
        }
    
       
        // Ammo Normal colour if Ammo is Above Number
        if (health <= 25)
        {
            countHealth.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }

        // Ammo Flashing Color if ammo is below number
        else if (health >= 26)
        {
            countHealth.color = new Color(1f, 1f, 1f, 1f);
        }

        // Lives Normal colour if Ammo is Above Number
        if (lives <= 0)
        {
            countLives.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }

        // Lives Flashing Color if ammo is below number
        else if (lives >= 1)
        {
            countLives.color = new Color(1f, 1f, 1f, 1f);
        }

        // flash Screen when damaged or collecting item
        if (redScreenFlashEnabled == true)
        {
            redScreen.SetActive(true);
            redScreenFlashTimer -= Time.deltaTime;
        }

        if (redScreenFlashTimer <= 0)
        {
            redScreen.SetActive(false);
            redScreenFlashEnabled = false;
            redScreenFlashTimer = redScreenFlashTimerStart;
        }

        if (whiteScreenFlashEnabled == true)
        {
            whiteScreen.SetActive(true);
            whiteScreenFlashTimer -= Time.deltaTime;
        }

        if (whiteScreenFlashTimer <= 0)
        {
            whiteScreen.SetActive(false);
            whiteScreenFlashEnabled = false;
            whiteScreenFlashTimer = whiteScreenFlashTimerStart;
        }

        // Keyboard Control for weapon number Key Switching
        if (Input.GetButtonDown("Num0") && Pistol == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(true);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = true;
            AssaultRifle = false;
            FlakCannon = false;
            PlasmaRifle = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;
            
            PistolGun.SetActive(true);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = true;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;
        }

        if (Input.GetButtonDown("Num1") && Assault >= 1 && AssaultRifle == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(true);
            BFGcountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            AssaultRifle = true;
            FlakCannon = false;
            PlasmaRifle = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            AssaultRifleGun.SetActive(true);
            FlakCannonGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = true;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;
        }

        else if (Input.GetButtonDown("Num2") && flak >= 1 && FlakCannon == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(true);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            FlakCannon = true;
            AssaultRifle = false;
            PlasmaRifle = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            FlakCannonGun.SetActive(true);
            AssaultRifleGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = true;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;
        }

        else if (Input.GetButtonDown("Num3") && plasma >= 1 && PlasmaRifle == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(true);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            PlasmaRifle = true;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(true);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = true;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;
        }
     
        else if (Input.GetButtonDown("Num4") && rail >= 1 && RailGun == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(true);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            PlasmaRifle = false; ;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = false;
            RailGun = true;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(true);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = true;
            RocketLauncherScript.enabled = false;
        }

        else if (Input.GetButtonDown("Num5") && rocket >= 1 && RocketLauncher == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(true);

            Pistol = false;
            PlasmaRifle = false; ;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = true;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(true);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = true;
        }

        else if (Input.GetButtonDown("Num6") && bfgproton >= 1 && BFG == false)
        {
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(true);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            PlasmaRifle = false; ;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = true;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(true);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = true;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;
        }
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EncounterTriggerBoss"))
        {
            quakeTimer += 5;
        }
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            PickupText.text = "CheckPoint";
            FadeTime += 5;
            LastCheckPoint = other.gameObject.transform.position;
            other.gameObject.SetActive(false);

        }
        // Compare Trigger if player hit by fireball
        if (other.gameObject.CompareTag("Lava"))
        {
            redScreen.SetActive(true);
            Lava = true;
        }

        // Compare Trigger for secret area
        if (other.gameObject.CompareTag("Secret"))
        {
            portal.Play();
            transform.position = new Vector3(38f, 1f, 448f);
        }

        // Compare trigger for secret area text
        if (other.gameObject.CompareTag("SecretPoint"))
        {
            PickupText.text = "Secret Found!";
            FadeTime += 5;
        }

        // compare trigger for portal to boss
        if (other.gameObject.CompareTag("Portal"))
        {
            portal.Play();
            transform.position = new Vector3(1195.2f, -29f, 479f);

        }

        // If player touches the final portal
        else if (other.gameObject.CompareTag("PortalFinished"))
        {
            portal.Play();
            SceneManager.LoadScene(1);
            Cursor.visible = true;
        }

        // Compare trigger for health pack
        else if (other.gameObject.CompareTag("HealthPack"))
        {
            PickupText.text = "Health +10";
            FadeTime += 5;
            AddHealth(10);
            other.gameObject.SetActive(false);
        }

        // compare trigger when player obtains Assault Rifle
        else if (other.gameObject.CompareTag("AssaultRifle"))
        {
            PickupText.text = "Assault Rifle";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            Assault += 1;
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(true);
            BFGcountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            AssaultRifle = true;
            FlakCannon = false;
            PlasmaRifle = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            AssaultRifleGun.SetActive(true);
            FlakCannonGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = true;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;

            other.gameObject.SetActive(false);
        }

        // compare trigger when player obtains Flak Cannon
        else if (other.gameObject.CompareTag("FlakCannon"))
        {
            PickupText.text = "Flak Cannon";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            flak += 1;
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(true);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            FlakCannon = true;
            AssaultRifle = false;
            PlasmaRifle = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            FlakCannonGun.SetActive(true);
            AssaultRifleGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = true;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;

            other.gameObject.SetActive(false);
        }

        // compare trigger when player obtains Plasma Rifle
        else if (other.gameObject.CompareTag("PlasmaRifle"))
        {
            PickupText.text = "Plasma Rifle";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            plasma += 1;
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(true);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            PlasmaRifle = true;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(true);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = true;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;

            other.gameObject.SetActive(false);
        }

        // compare trigger when player obtains Rail Gun
        else if (other.gameObject.CompareTag("RailGun"))
        {
            PickupText.text = "Rail Gun";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            rail += 1;
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(true);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            PlasmaRifle = false; ;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = false;
            RailGun = true;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(true);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = true;
            RocketLauncherScript.enabled = false;

            other.gameObject.SetActive(false);
        }

        // compare trigger when player obtains Rocket Launcher
        else if (other.gameObject.CompareTag("RocketLauncher"))
        {
            PickupText.text = "Rocket Launcher";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            rocket += 1;
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(false);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(true);

            Pistol = false;
            PlasmaRifle = false; ;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = false;
            RailGun = false;
            RocketLauncher = true;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(false);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(true);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = false;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = true;

            other.gameObject.SetActive(false);
        }

        // compare trigger when player obtains BFG Proton
        else if (other.gameObject.CompareTag("BFGProton"))
        {
            PickupText.text = "BFG Proton";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            bfgproton += 1;
            ammoSound.Play();

            PistolcountAmmo.SetActive(false);
            AssaultcountAmmo.SetActive(false);
            BFGcountAmmo.SetActive(true);
            PlasmacountAmmo.SetActive(false);
            FlakcountAmmo.SetActive(false);
            RailcountAmmo.SetActive(false);
            RocketcountAmmo.SetActive(false);

            Pistol = false;
            PlasmaRifle = false; ;
            AssaultRifle = false;
            FlakCannon = false;
            BFG = true;
            RailGun = false;
            RocketLauncher = false;

            PistolGun.SetActive(false);
            PlasmaRifleGun.SetActive(false);
            AssaultRifleGun.SetActive(false);
            FlakCannonGun.SetActive(false);
            BFGProtonGun.SetActive(true);
            railGunGun.SetActive(false);
            rocketLauncherGun.SetActive(false);

            PistolScript.enabled = false;
            AssaultRifleScript.enabled = false;
            FlakCannonScript.enabled = false;
            PlasmaRifleScript.enabled = false;
            BFGProtonScript.enabled = true;
            RailGunScript.enabled = false;
            RocketLauncherScript.enabled = false;

            other.gameObject.SetActive(false);
        }

        // compare trigger when player Picks up Rifle Ammo
        else if (other.gameObject.CompareTag("AmmoPack"))
        {
            PickupText.text = "Picked Up Rifle Ammo";
            FadeTime += 5;
        }

        // compare trigger when player Picks up Flak Ammo
        else if (other.gameObject.CompareTag("FlakAmmoPack"))
        {
            PickupText.text = "Picked Up Flak Shells";
            FadeTime += 5;
        }

        // compare trigger when player Picks up Plasma Ammo
        else if (other.gameObject.CompareTag("PlasmaAmmoPack"))
        {
            PickupText.text = "Picked Up Plasma Cells";
            FadeTime += 5;
        }

        // compare trigger when player Picks up Rail Ammo
        else if (other.gameObject.CompareTag("RailAmmoPack"))
        {
            PickupText.text = "Picked Up Slugs";
            FadeTime += 5;

        }

        // compare trigger when player Picks up Rocket Ammo
        else if (other.gameObject.CompareTag("RocketAmmoPack"))
        {
            PickupText.text = "Picked Up Rockets";
            FadeTime += 5;

        }

        // compare trigger when player Picks up BFG Ammo
        else if (other.gameObject.CompareTag("BFGAmmoPack"))
        {
            PickupText.text = "Picked Up Proton Cells";
            FadeTime += 5;
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            redScreen.SetActive(false);
            Lava = false;
        }
        // when player exits the secret point trigger
        if (other.gameObject.CompareTag("SecretPoint"))
        {
            other.gameObject.SetActive(false);
        }
    }
  

    // Setting the Lives to the canvas
    void SetCountLives()
    {
        countLives.text = lives.ToString();
    }

    // Setting the Health to the canvas
    void SetCountHealth()
    {
        countHealth.text = Mathf.CeilToInt(health).ToString() + " %";
    }
    void ShutOffAllWeapons()
    {
        PistolcountAmmo.SetActive(false);
        AssaultcountAmmo.SetActive(false);
        BFGcountAmmo.SetActive(false);
        FlakcountAmmo.SetActive(false);
        PlasmacountAmmo.SetActive(false);
        RailcountAmmo.SetActive(false);
        RocketcountAmmo.SetActive(false);

        Pistol = false;
        AssaultRifle = false;
        FlakCannon = false;
        PlasmaRifle = false;
        BFG = false;
        RailGun = false;
        RocketLauncher = false;

        PistolGun.SetActive(false);
        AssaultRifleGun.SetActive(false);
        FlakCannonGun.SetActive(false);
        PlasmaRifleGun.SetActive(false);
        BFGProtonGun.SetActive(false);
        railGunGun.SetActive(false);
        rocketLauncherGun.SetActive(false);

        PistolScript.enabled = false;
        AssaultRifleScript.enabled = false;
        FlakCannonScript.enabled = false;
        PlasmaRifleScript.enabled = false;
        BFGProtonScript.enabled = false;
        RailGunScript.enabled = false;
        RocketLauncherScript.enabled = false;
    }
    void SetupPistol()
    {
        PistolcountAmmo.SetActive(true);
        Pistol = true;
        PistolGun.SetActive(true);
        PistolScript.enabled = true;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyFireBall"))
        {
            EnemyFireBall = true;
            hurtPlayer = true;
            redScreenFlashEnabled = true;
            deathSound.Play();
        }
        if (collision.gameObject.CompareTag("Fireball"))
        {
            FireBall = true;
            hurtPlayer = true;
            redScreenFlashEnabled = true;
            deathSound.Play();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy = true;
            hurtPlayer = true;
            redScreenFlashEnabled = true;
            deathSound.Play();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
        public void DoDamage(float amount)
    {
        health -= amount;
        redScreenFlashEnabled = true;

        if (health <= 0)
        {
            gameObject.transform.position = LastCheckPoint;
            lives -= 1;
            health = 100;
            BossFireBallDam = false;
            Lava = false;
            redScreen.SetActive(false);
            deathSound.Play();
        }
        if (lives <= 0)
        {
            health = 0;
            lives = 0;
            //SceneManager.LoadScene(1);
        }
        SetCountLives();
        SetCountHealth();
    }
    public void AddHealth(float amount)
    {
        health += amount;
        whiteScreenFlashEnabled = true;
        
        if (health >= 100)
        {
            health = 100;
        }
        SetCountHealth();
        healthSound.Play();
    }

}