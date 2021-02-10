using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AssaultRifle : MonoBehaviour
{
    public AudioSource Shoot;
    public Camera CameraPosition;
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public GameObject whiteScreen;
    public AudioSource ammoSound;
    public float Bullet_Forward_Force;
    public GameObject MuzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public int Ammo;
    public bool whiteScreenFlashEnabled = false;
    public Text AssaultAmmo;
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
    public GameObject AssaultRifleGun;
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
        if (Ammo >= 200)
        {
            Ammo = 200;
            SetCountAmmo();
        }
        if (Ammo <= 0)
        {
            Ammo = 0;
            AmmoShot = false;
            SetCountAmmo();
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
                AssaultRifleGun.SetActive(false);
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
                AssaultRifleGun.SetActive(true);
                AmmoShot = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseText.text = "";
                levelMusic.Play();
            }
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true)
        {
            recoilComponent.StartRecoil(0.01f, -2f, 40f);
            Ammo -= 1;
            SetCountAmmo();
            nextFire = Time.time + fireRate;
            muzzleFlashEnabled = true;
            Shoot.Play();
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left* 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 10.0f);
        }
        if (Ammo <= 25)
        {
            AssaultAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Ammo >= 26)
        {
            AssaultAmmo.color = new Color(1f, 1f, 1f, 1f);
        }
        if (muzzleFlashEnabled == true)
        {
            
            MuzzleFlashObject.SetActive(true);
            MuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
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
        if (other.gameObject.CompareTag("AmmoPack"))
        {
            AmmoShot = true;
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 50;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                AssaultRifleGun.SetActive(true);
            }
            if (Ammo == 200)
            {
                Ammo = 200;
            }
        }
        if (other.gameObject.CompareTag("AssaultRifle"))
        {
            AmmoShot = true;
            whiteScreenFlashEnabled = true;
            Ammo = Ammo + 100;
            SetCountAmmo();
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                AssaultRifleGun.SetActive(true);
            }
            if (Ammo == 200)
            {
                Ammo = 200;
            }
        }
        SetCountAmmo();
    }
    void SetCountAmmo()
    {
        AssaultAmmo.text = "" + Ammo.ToString();
    }
}


   