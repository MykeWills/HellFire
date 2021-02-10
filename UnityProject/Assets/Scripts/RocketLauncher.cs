using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RocketLauncher : MonoBehaviour
{
    public AudioSource Shoot;
    public AudioSource Reload;
    public Camera CameraPosition;
    public GameObject whiteScreen;
    public AudioSource ammoSound;
    public GameObject SlotOne_Bullet_Emitter;
    public GameObject SlotTwo_Bullet_Emitter;
    public GameObject SlotThree_Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    public GameObject SlotOneMuzzleFlashObject;
    public GameObject SlotTwoMuzzleFlashObject;
    public GameObject SlotThreeMuzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public GameObject Rocket;
    public GameObject Rocket2;
    public GameObject Rocket3;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public int Ammo = 400;
    public bool whiteScreenFlashEnabled = false;
    public Text RocketcountAmmo;
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
    public GameObject RocketLauncherGun;
  
    public bool RocketSlot0;
    public bool RocketSlot1;
    public bool RocketSlot2;

    bool BarrelSpin;
    public int BarrelCounter;
    public float Axisz;
    private Recoil recoilComponent;

    public GameObject Barrel;
    public float SpinSpeed;
    bool GotAmmo;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RocketSlot0 = true;
        RocketSlot1 = false;
        RocketSlot2 = false;
        BarrelSpin = false;
        BarrelCounter = 0;
        var cam = GameObject.Find("/Player/MainCamera/Recoil/").transform;
        recoilComponent = cam.GetComponent<Recoil>();
        muzzleFlashTimerStart = muzzleFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        AmmoShot = true;
        GotAmmo = false;
        SetCountAmmo();
        
    }

    // Update is called once per frame
    public void Update()
    {
        Axisz = Barrel.transform.rotation.eulerAngles.z;
        if (GotAmmo)
        {
            SpinSpeed = 200;
            float theta = Time.deltaTime * SpinSpeed;
            Barrel.transform.Rotate(Vector3.forward, theta);
            if (BarrelSpin && BarrelCounter == 0 && AmmoShot)
            {
                Barrel.transform.Rotate(Vector3.forward, theta);
                Mathf.Clamp(Axisz, 240, 240);
            }
            if (Axisz > 237 && BarrelCounter == 0 && AmmoShot)
            {
                GotAmmo = false;
                BarrelSpin = false;
                BarrelCounter = 1;
            }
            if (BarrelSpin && BarrelCounter == 1 && AmmoShot)
            {
                Barrel.transform.Rotate(Vector3.forward, theta);
                Mathf.Clamp(Axisz, 0, 0);
            }
            if (Axisz > 0 && Axisz < 6 && BarrelCounter == 1 && AmmoShot)
            {
                GotAmmo = false;
                BarrelSpin = false;
                BarrelCounter = 2;
            }
            if (BarrelSpin && BarrelCounter == 2 && AmmoShot)
            {
                Barrel.transform.Rotate(Vector3.forward, theta);
                Mathf.Clamp(Axisz, 120, 120);
            }
            if (Axisz >= 117 && BarrelCounter == 2 && AmmoShot)
            { 
                GotAmmo = false;
                BarrelSpin = false;
                BarrelCounter = 0;
            }
        }

        if (BarrelSpin && !AmmoShot)
        {
            SpinSpeed = 50;
            float theta = Time.deltaTime * SpinSpeed;
            Barrel.transform.Rotate(Vector3.forward, theta);
        }
        if (BarrelSpin && BarrelCounter == 0 && AmmoShot)
        {
            SpinSpeed = 200;
            float theta = Time.deltaTime * SpinSpeed;
            Barrel.transform.Rotate(Vector3.forward, theta);
            Mathf.Clamp(Axisz, 240, 240);
        }
        if (Axisz > 237 && BarrelCounter == 0 && AmmoShot)
        {
            BarrelSpin = false;
            BarrelCounter = 1;
        }
        if (BarrelSpin && BarrelCounter == 1 && AmmoShot)
        {
            
            float theta = Time.deltaTime * SpinSpeed;
            Barrel.transform.Rotate(Vector3.forward, theta);
            Mathf.Clamp(Axisz, 0, 0);
        }
        if (Axisz > 0 && Axisz < 6 && BarrelCounter == 1 && AmmoShot)
        {
            BarrelSpin = false;
            BarrelCounter = 2;
        }


        if (BarrelSpin && BarrelCounter == 2 && AmmoShot)
        {

            float theta = Time.deltaTime * SpinSpeed;
            Barrel.transform.Rotate(Vector3.forward, theta);
            Mathf.Clamp(Axisz, 120, 120);
        }
        if (Axisz >= 117 && BarrelCounter == 2 && AmmoShot)
        {
            BarrelSpin = false;
            BarrelCounter = 0;
        }



        if (Ammo >= 10)
        {
            Ammo = 10;
            SetCountAmmo();
        }
        if (Ammo <= 0)
        {
            Ammo = 0;
            Rocket.SetActive(false);
            Rocket2.SetActive(false);
            Rocket3.SetActive(false);
            BarrelSpin = true;
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
                RocketLauncherGun.SetActive(false);
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
                RocketLauncherGun.SetActive(true);
                AmmoShot = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseText.text = "";
                levelMusic.Play();
            }
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true && RocketSlot0 == true)
        {
            BarrelSpin = true;
            Bullet.SetActive(true);
            recoilComponent.StartRecoil(0.5f, -20f, 10f);
            Ammo -= 1;
            SetCountAmmo();
            nextFire = Time.time + fireRate;

            muzzleFlashEnabled = true;
            Shoot.Play();
            //The Bullet instantiation happens here.
            //GameObject Temporary_Bullet_Handler;
            Rocket3.SetActive(true);
            Rocket.SetActive(false);
            Rocket2.SetActive(true);
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, SlotOne_Bullet_Emitter.transform.position, SlotOne_Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 4.0f);
            RocketSlot0 = false;
            RocketSlot1 = true;
            RocketSlot2 = false;

        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true && RocketSlot1 == true)
        {
            BarrelSpin = true;
            Bullet.SetActive(true);
            recoilComponent.StartRecoil(0.5f, -20f, 10f);
            Ammo -= 1;
            SetCountAmmo();
            nextFire = Time.time + fireRate;
            muzzleFlashEnabled = true;
            Shoot.Play();
            //The Bullet instantiation happens here.
            //GameObject Temporary_Bullet_Handler;
            Rocket2.SetActive(false);
            Rocket3.SetActive(true);
            Rocket.SetActive(true);
           
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, SlotTwo_Bullet_Emitter.transform.position, SlotTwo_Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 4.0f);
            RocketSlot0 = false;
            RocketSlot1 = false;
            RocketSlot2 = true;
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true && RocketSlot2 == true)
        {
            BarrelSpin = true;
            Bullet.SetActive(true);
            recoilComponent.StartRecoil(0.5f, -20f, 10f);
            Ammo -= 1;
            SetCountAmmo();
            nextFire = Time.time + fireRate;

            if (Ammo <= 0)
            {
                Ammo = 0;
                Rocket.SetActive(false);
                Rocket2.SetActive(false);
                Rocket3.SetActive(false);
                RocketLauncherGun.SetActive(false);
                AmmoShot = false;
            }
            muzzleFlashEnabled = true;
            Shoot.Play();
            //The Bullet instantiation happens here.
            //GameObject Temporary_Bullet_Handler;
            Rocket3.SetActive(false);
            Rocket.SetActive(true);
            Rocket2.SetActive(true);
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, SlotThree_Bullet_Emitter.transform.position, SlotThree_Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 4.0f);
            RocketSlot0 = true;
            RocketSlot1 = false;
            RocketSlot2 = false;
        }

        if (Time.time > nextFire && AmmoShot == true)
        {

            Rocket.SetActive(true);
            Rocket2.SetActive(true);
            Rocket3.SetActive(true);
            
        }
        if (Ammo <= 1)
        {
            RocketcountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Ammo >= 2)
        {
            RocketcountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }
        if (muzzleFlashEnabled == true && RocketSlot0 == true)
        {
            //Reload.Play();
            SlotOneMuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            Rocket3.SetActive(true);
            Rocket.SetActive(false);
            Rocket2.SetActive(true);
            SlotOneMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashEnabled == true && RocketSlot1 == true)
        {
            //Reload.Play();
            SlotTwoMuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            Rocket3.SetActive(true);
            Rocket.SetActive(true);
            Rocket2.SetActive(false);
            SlotTwoMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashEnabled == true && RocketSlot2 == true)
        {
            //Reload.Play();
            SlotThreeMuzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            Rocket3.SetActive(false);
            Rocket.SetActive(true);
            Rocket2.SetActive(true);
            SlotThreeMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {

            SlotOneMuzzleFlashObject.SetActive(false);
            SlotTwoMuzzleFlashObject.SetActive(false);
            SlotThreeMuzzleFlashObject.SetActive(false);
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
        if (other.gameObject.CompareTag("RocketAmmoPack"))
        {
            AmmoShot = true;
            Rocket.SetActive(true);
            Rocket2.SetActive(true);
            Rocket3.SetActive(true);
            BarrelSpin = false;
            SpinSpeed = 200;
            whiteScreenFlashEnabled = true;
            ammoSound.Play();
            Ammo = Ammo + 2;
            GotAmmo = true;
            other.gameObject.SetActive(false);
           
            if (Ammo == 10)
            {
                Ammo = 10;

            }
        }
        else if (other.gameObject.CompareTag("RocketLauncher"))
        {
            AmmoShot = true;
            Rocket.SetActive(true);
            Rocket2.SetActive(true);
            Rocket3.SetActive(true);
            whiteScreenFlashEnabled = true;
            BarrelSpin = false;
            SpinSpeed = 200;
            ammoSound.Play();
            Ammo = Ammo + 5;
            GotAmmo = true;
            other.gameObject.SetActive(false);
            if (Ammo == 10)
            {
                Ammo = 10;
            }
        }
        SetCountAmmo();
    }
    void SetCountAmmo()
    {
        RocketcountAmmo.text = "" + Ammo.ToString();
    }
}