using UnityEngine;
using System.Collections;

public class AutoFire : MonoBehaviour
{
    
    public AudioSource Shoot;
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;
    bool startShooting;
    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;
    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;
    public float shotTime = 1.0f;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotTime -= Time.deltaTime;

        if(shotTime <= 0)
        {
            Shoot.Play();
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler, 10.0f);
            shotTime = shotTime + 2;
        }
    }
}