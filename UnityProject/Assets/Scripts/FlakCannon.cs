using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlakCannon : MonoBehaviour
{
    public AudioSource Shoot;
    public Camera CameraPosition;
    public GameObject Bullet_Emitter1;
    public GameObject Bullet_Emitter2;
    public GameObject Bullet_Emitter3;
    public GameObject Bullet_Emitter4;
    public GameObject Bullet_Emitter5;
    public GameObject ShellBullet;
    public GameObject whiteScreen;
    public AudioSource ammoSound;
    public int Bullet_Forward_Force1;
    public int Bullet_Forward_Force2;
    public int Bullet_Forward_Force3;
    public int Bullet_Forward_Force4;
    public int Bullet_Forward_Force5;

    public GameObject MuzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public int Ammo;
    public bool whiteScreenFlashEnabled = false;
    public Text FlakcountAmmo;
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
    public GameObject FlakCannonGun;
    public GameObject railGun;
    public GameObject FlakShell;
    private Recoil recoilComponent;
  
    public int Bullet_Side_Force1;
    public int Bullet_Side_Force2;
    public int Bullet_Side_Force3;
    public int Bullet_Side_Force4;
    public int Bullet_Side_Force5;

    public int Bullet_Up_Force1;
    public int Bullet_Up_Force2;
    public int Bullet_Up_Force3;
    public int Bullet_Up_Force4;
    public int Bullet_Up_Force5;

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
    //&& Time.time > nextFire
    // Update is called once per frame
    public void Update()
    {
        if (Time.time > nextFire && AmmoShot == true)
        {
            FlakShell.SetActive(true);
        }
        if (Ammo >= 60)
        {
            Ammo = 60;
            SetCountAmmo();
        }
        if (Ammo <= 0)
        {
            FlakShell.SetActive(false);
            Ammo = 0;
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
                FlakCannonGun.SetActive(false);
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
                FlakCannonGun.SetActive(true);
                AmmoShot = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseText.text = "";
                levelMusic.Play();
            }
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true)
        {
            FlakShell.SetActive(false);
            recoilComponent.StartRecoil(0.2f, -10f, 10f);
            Ammo -= 1;
            SetCountAmmo();
            nextFire = Time.time + fireRate;
           
            muzzleFlashEnabled = true;
            //nextFire = Time.time + fireRate;
            Shoot.Play();
            RandomizeForce();

            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(ShellBullet, Bullet_Emitter1.transform.position, Bullet_Emitter1.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force1);
            Temporary_RigidBody.AddForce(CameraPosition.transform.right * Bullet_Side_Force1);
            Temporary_RigidBody.AddForce(CameraPosition.transform.up * Bullet_Up_Force1);
            Destroy(Temporary_Bullet_Handler, 10.0f);

            GameObject Temporary_Bullet_Handler2;
            Temporary_Bullet_Handler2 = Instantiate(ShellBullet, Bullet_Emitter2.transform.position, Bullet_Emitter2.transform.rotation) as GameObject;
            Temporary_Bullet_Handler2.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody2;
            Temporary_RigidBody2 = Temporary_Bullet_Handler2.GetComponent<Rigidbody>();
            Temporary_RigidBody2.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force2);
            Temporary_RigidBody2.AddForce(CameraPosition.transform.right * Bullet_Side_Force2);
            Temporary_RigidBody2.AddForce(CameraPosition.transform.up * Bullet_Up_Force2);
            Destroy(Temporary_Bullet_Handler2, 10.0f);

            GameObject Temporary_Bullet_Handler3;
            Temporary_Bullet_Handler3 = Instantiate(ShellBullet, Bullet_Emitter3.transform.position, Bullet_Emitter3.transform.rotation) as GameObject;
            Temporary_Bullet_Handler3.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody3;
            Temporary_RigidBody3 = Temporary_Bullet_Handler3.GetComponent<Rigidbody>();
            Temporary_RigidBody3.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force3);
            Temporary_RigidBody3.AddForce(CameraPosition.transform.right * Bullet_Side_Force3);
            Temporary_RigidBody3.AddForce(CameraPosition.transform.up * Bullet_Up_Force3);
            Destroy(Temporary_Bullet_Handler3, 10.0f);

            GameObject Temporary_Bullet_Handler4;
            Temporary_Bullet_Handler4 = Instantiate(ShellBullet, Bullet_Emitter4.transform.position, Bullet_Emitter4.transform.rotation) as GameObject;
            Temporary_Bullet_Handler4.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody4;
            Temporary_RigidBody4 = Temporary_Bullet_Handler4.GetComponent<Rigidbody>();
            Temporary_RigidBody4.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force4);
            Temporary_RigidBody4.AddForce(CameraPosition.transform.right * Bullet_Side_Force4);
            Temporary_RigidBody4.AddForce(CameraPosition.transform.up * Bullet_Up_Force4);
            Destroy(Temporary_Bullet_Handler4, 10.0f);


            GameObject Temporary_Bullet_Handler5;
            Temporary_Bullet_Handler5 = Instantiate(ShellBullet, Bullet_Emitter5.transform.position, Bullet_Emitter5.transform.rotation) as GameObject;
            Temporary_Bullet_Handler5.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody5;
            Temporary_RigidBody5 = Temporary_Bullet_Handler5.GetComponent<Rigidbody>();
            Temporary_RigidBody5.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force5);
            Temporary_RigidBody5.AddForce(CameraPosition.transform.right * Bullet_Side_Force5);
            Temporary_RigidBody5.AddForce(CameraPosition.transform.up * Bullet_Up_Force5);
            Destroy(Temporary_Bullet_Handler5, 10.0f);

           
        }
        if (Ammo <= 10)
        {
            FlakcountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Ammo >= 11)
        {
            FlakcountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }
        if (muzzleFlashEnabled == true)
        {
            FlakShell.SetActive(false);
            MuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
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
        if (other.gameObject.CompareTag("FlakAmmoPack"))
        {
            AmmoShot = true;
            FlakShell.SetActive(true);
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 15;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                FlakCannonGun.SetActive(true);
            }
            if (Ammo == 60)
            {
                Ammo = 60;
            }
        }
        else if (other.gameObject.CompareTag("FlakCannon"))
        {
            FlakShell.SetActive(true);
            AmmoShot = true;
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 30;
            other.gameObject.SetActive(false);
            if (Ammo == 0)
            {
                FlakCannonGun.SetActive(true);
            }
            if (Ammo == 60)
            {
                Ammo = 60;
            }
        }
        SetCountAmmo();
    }
    void SetCountAmmo()
    {
        FlakcountAmmo.text = "" + Ammo.ToString();
    }
    void RandomizeForce()
    {
        Bullet_Forward_Force1 = Random.Range(5000, 12000);
        Bullet_Forward_Force2 = Random.Range(5000, 12000);
        Bullet_Forward_Force3 = Random.Range(5000, 12000);
        Bullet_Forward_Force4 = Random.Range(5000, 12000);
        Bullet_Forward_Force5 = Random.Range(5000, 12000);
        Bullet_Side_Force1 = Random.Range(-690, 690);
        Bullet_Side_Force2 = Random.Range(-690, 690);
        Bullet_Side_Force3 = Random.Range(-690, 690);
        Bullet_Side_Force4 = Random.Range(-690, 690);
        Bullet_Side_Force5 = Random.Range(-690, 690);


        Bullet_Up_Force1 = Random.Range(-500, 500);
        Bullet_Up_Force2 = Random.Range(-500, 500);
        Bullet_Up_Force3 = Random.Range(-500, 500);
        Bullet_Up_Force4 = Random.Range(-500, 500);
        Bullet_Up_Force5 = Random.Range(-500, 500);
    }
}