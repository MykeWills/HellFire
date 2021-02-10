using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSlug : MonoBehaviour {
    public GameObject RailTrail;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject expl = Instantiate(RailTrail, transform.position, Quaternion.identity) as GameObject;
        Destroy(expl, 3);
    }
    void OnCollisionEnter()
    {
        
        Destroy(gameObject); // destroy the grenade

    }
}
