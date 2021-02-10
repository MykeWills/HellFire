using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    public AudioSource ExplosionSound;
    public GameObject explosion;
    public GameObject Bullet;
    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    void OnCollisionEnter()
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        ExplosionSound.Play();
        Bullet.SetActive(true);
        Destroy(gameObject); // destroy the grenade
        Destroy(expl, 3); // delete the explosion after 3 seconds
        
    }
}
