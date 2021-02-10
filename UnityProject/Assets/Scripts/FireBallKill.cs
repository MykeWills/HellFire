using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallKill : MonoBehaviour {
    public int BallHealth;
    public GameObject Projectile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (BallHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            BallHealth -= 1;

        }
        else if (other.gameObject.CompareTag("FlakBullet"))
        {
            Destroy(other.gameObject);
            BallHealth -= 2;


        }
        else if (other.gameObject.CompareTag("PlasmaBullet"))
        {

            Destroy(other.gameObject);
            BallHealth -= 5;
        }
        else if (other.gameObject.CompareTag("BFG"))
        {
            Destroy(other.gameObject);
            BallHealth -= 100;
        }
        else if (other.gameObject.CompareTag("RailSlug"))
        {
            Destroy(other.gameObject);
            BallHealth -= 25;
        }
        else if (other.gameObject.CompareTag("Rocket"))
        {
            Destroy(other.gameObject);
            BallHealth -= 50;
        }
    }
}
