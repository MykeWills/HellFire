using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RailGun : MonoBehaviour
{
    public AudioSource Shoot;
    public AudioSource Reload;
    public bool reloadGun;
    //public AudioSource Reload;
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
    public GameObject Slug;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public int Ammo = 400;
    public bool whiteScreenFlashEnabled = false;
    public Text RailcountAmmo;
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
    public GameObject railGun;
    private Recoil recoilComponent;
   


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        var cam = GameObject.Find("/Player/MainCamera/Recoil/").transform;
        recoilComponent = cam.GetComponent<Recoil>();

        muzzleFlashTimerStart = muzzleFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        AmmoShot = true;
        SetCountAmmo();

    }

    // Update is called once per frame
    public void Update()
    {
        if (Ammo >= 20)
        {
            Ammo = 20;
            SetCountAmmo();
        }
        if (Ammo <= 0)
        {
            Ammo = 0;
            SetCountAmmo();
            Slug.SetActive(false);
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
                railGun.SetActive(false);
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
                railGun.SetActive(true);
                AmmoShot = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseText.text = "";
                levelMusic.Play();
            }
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true)
        {
            recoilComponent.StartRecoil(0.2f, -15f, 10f);
            Ammo -= 1;
            SetCountAmmo();
            nextFire = Time.time + fireRate;
            Slug.SetActive(false);
           
            muzzleFlashEnabled = true;
            Reload.Stop();
            Shoot.Play();
            reloadGun = true;

            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 4.0f);
        }
        if (Time.time > nextFire && AmmoShot == true)
        {
            Slug.SetActive(true);
            if (reloadGun)
            {
                Reload.Play();
                reloadGun = false;
            }
           
        }
        if (Ammo <= 1)
        {
            RailcountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Ammo >= 2)
        {
            RailcountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }
        if (muzzleFlashEnabled == true)
        {
            //Reload.Play();
            Slug.SetActive(false);
            //MuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RailAmmoPack"))
        {
            AmmoShot = true;
            Slug.SetActive(true);
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 5;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                railGun.SetActive(false);
            }
            if (Ammo == 20)
            {
                Ammo = 20;
            }
        }
        else if (other.gameObject.CompareTag("RailGun"))
        {
            AmmoShot = true;
            Slug.SetActive(true);
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 10;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                railGun.SetActive(false);
            }
            if (Ammo == 20)
            {
                Ammo = 20;
            }
        }
        SetCountAmmo();
    }
    void SetCountAmmo()
    {
        RailcountAmmo.text = "" + Ammo.ToString();
    }

    
}