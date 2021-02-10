using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BFGProton : MonoBehaviour
{
    public AudioSource Shoot;
    public AudioSource ChargeUp;
    public AudioSource ChargingUp;
    public AudioSource QuakeSound;
    public bool shot;
    public Camera CameraPosition;
    public GameObject whiteScreen;
    public AudioSource ammoSound;
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    public GameObject MuzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public GameObject ProtonBall;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public int Ammo;
    public bool whiteScreenFlashEnabled = false;
    public Text BFGcountAmmo;
    public Text pauseText;
    public GameObject OptionsMenu;
    public GameObject ConfirmMenu;
    public GameObject AudioMenu;
    public GameObject VideoMenu;
    public GameObject ControlsMenu;
    public GameObject CursorCrosshair;
    public bool AmmoShot;
    public bool paused;
    public AudioSource levelMusic;
    public float fireRate;
    private float nextFire;
    public GameObject BFGProtonGun;
    private Recoil recoilComponent;
    public ParticleSystem ps;
    public bool GunCharge;
    public float ChargeUpTime = 2;
    public float ChargeUpTimer;
    public float ChargeSpeed;
    bool Charging;
    bool PowerUp;
    bool PowerDown;

    public float ShakeAmount;
    public float GunShakeAmount;
    public float ShakeTimer;
    public float ShakeTime;
    public float ShakeSpeed;
    public float muzzleRotateSpeed;


    Transform camTransform;
    Transform gunTransform;
    Vector3 camOriginalPos;
    Vector3 gunOriginalPos;
    bool SetGun;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        var cam = GameObject.Find("/Player/MainCamera/Recoil/").transform;
        camTransform = Camera.main.transform;
        gunTransform = GameObject.Find("/Player/MainCamera/Recoil/Hover/Gun/").transform;
        camOriginalPos = camTransform.localPosition;
        gunOriginalPos = gunTransform.localPosition;
        recoilComponent = cam.GetComponent<Recoil>();
        muzzleFlashTimerStart = muzzleFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        AmmoShot = true;
        SetCountAmmo();
    }

    // Update is called once per frame
    public void Update()
    {
       

        if (Ammo >= 8)
        {
            Ammo = 8;
            SetCountAmmo();
        }
        if (Ammo <= 0)
        {
            Ammo = 0;
            ProtonBall.SetActive(false);
            SetCountAmmo();
            AmmoShot = false;
        }
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            if (Input.GetMouseButtonDown(0) && paused == true)
            {
                Ammo -= 0;
            }
            if (paused == true)
            {
                AmmoShot = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                CursorCrosshair.SetActive(false);
                OptionsMenu.SetActive(true);
                BFGProtonGun.SetActive(false);
                pauseText.text = "Game Paused";
                levelMusic.Pause();
            }
            else
            {
                OptionsMenu.SetActive(false);
                ConfirmMenu.SetActive(false);
                AudioMenu.SetActive(false);
                VideoMenu.SetActive(false);
                ControlsMenu.SetActive(false);
                CursorCrosshair.SetActive(true);
                BFGProtonGun.SetActive(true);
                AmmoShot = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseText.text = "";
                levelMusic.Play();
            }
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true)
        {
           
            GunCharge = true;
            if (GunCharge)
            {
                SetGun = true;
                ChargeUpTimer = Mathf.Clamp(ChargeUpTimer + Time.deltaTime, 0, ChargeUpTime);
                ShakeTimer = Mathf.Clamp(ShakeTimer + Time.deltaTime, 0, ShakeTime);
                if (ChargeUpTimer > 0 && ChargeUpTimer < 0.1)
                {
                    PowerUp = true;
                }
                if (ChargeUpTimer > 1 && shot)
                {
                    ChargingUp.Play();
                    shot = false;
                }
                //if (ChargeUpTimer > 2.5 && shot)
                //{

                //    if (shot)
                //    {
                //        Shoot.Play();
                //        shot = false;
                //    }
                //}

                if (ChargeUpTimer >= ChargeUpTime && Time.time > nextFire)
                {
                    QuakeSound.Play();
                    Shoot.Play();
                    Bullet.SetActive(true);
                    recoilComponent.StartRecoil(0.5f, -20f, 10f);
                    Ammo -= 1;
                    SetCountAmmo();
                    nextFire = Time.time + fireRate;
                    ProtonBall.SetActive(false);
                    muzzleFlashEnabled = true;
                    camTransform.localPosition = camOriginalPos;
                    GameObject Temporary_Bullet_Handler;
                    Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
                    Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
                    Rigidbody Temporary_RigidBody;
                    Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);
                    Destroy(Temporary_Bullet_Handler, 4.0f);
                }
               
                MuzzleFlashObject.SetActive(true);
                ps.gameObject.SetActive(true);


            }
        }
        else
        {
            shot = true;
            ps.gameObject.SetActive(false);
            ChargeUpTimer = 0;
            ShakeTimer = 0;
            GunCharge = false;
            MuzzleFlashObject.SetActive(false);
            ChargingUp.Stop();
            ChargeUp.Stop();
            camTransform.localPosition = camOriginalPos;
            if (SetGun)
            {
                gunTransform.localPosition = gunOriginalPos;
                SetGun = false;
            }
            
            ChargeUpTimer = Mathf.Clamp(ChargeUpTimer - Time.deltaTime, 0, ChargeUpTime);
            ShakeTimer = Mathf.Clamp(ShakeTimer - Time.deltaTime, 0, ShakeTime);
            if (ChargeUpTimer < 2f && ChargeUpTimer > 1.8f)
            {
                
                //PlayerAudSrc.clip = MiniGunSpinDown;
                //PlayerAudSrc.loop = false;
                PowerDown = true;
            }
        }
        
        if (ShakeTimer > 0 && GunCharge)
        {
            camTransform.localPosition = camOriginalPos + Random.insideUnitSphere * ShakeTimer / 2;
            gunTransform.localPosition = gunOriginalPos + Random.insideUnitSphere * GunShakeAmount;
        }
        if (PowerUp)
        {
           
            ChargeUp.Play();
         
            
         
            PowerUp = false;
        }
        if (PowerDown)
        {
            //PlayerAudSrc.PlayOneShot(MiniGunSpinDown, 0.7f);
            PowerDown = false;
        }
        
        if (Time.time > nextFire && AmmoShot == true)
        {
            ProtonBall.SetActive(true);
           
        }
        if (Ammo <= 1)
        {
            BFGcountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Ammo >= 2)
        {
            BFGcountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }
        if (muzzleFlashEnabled == true)
        {

            //MuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            ProtonBall.SetActive(false);
            MuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {

            MuzzleFlashObject.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;

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

    }
    public void LateUpdate()
    {
        RotateMuzzle();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BFGAmmoPack"))
        {
            AmmoShot = true;
            ProtonBall.SetActive(true);
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 1;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                BFGProtonGun.SetActive(true);
            }
            if (Ammo == 8)
            {
                Ammo = 8;
            }
        }
        else if (other.gameObject.CompareTag("BFGProton"))
        {
            AmmoShot = true;
            ProtonBall.SetActive(true);
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 4;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                BFGProtonGun.SetActive(true);
            }
            if (Ammo == 8)
            {
                Ammo = 8;
               
            }
        }
        SetCountAmmo();
    }
    void SetCountAmmo()
    {
        BFGcountAmmo.text = "" + Ammo.ToString();
    }
    void RotateMuzzle()
    {
        var main = ps.main;
        muzzleRotateSpeed = ShakeTimer;
        MuzzleFlashObject.transform.Rotate(Vector3.up * Time.deltaTime * ChargeUpTimer * 500, Space.Self);
        main.simulationSpeed = Time.deltaTime * ChargeUpTimer * 20;


    }
}