using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileCollision : MonoBehaviour {
    public GameObject explosion;
    private DamageRadius DamageRadiusScript;
    public GameObject FireBallBoss;
    // Use this for initialization
    void Start () {
        DamageRadiusScript = FireBallBoss.GetComponent<DamageRadius>();

    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnCollisionEnter()
    {
        DamageRadiusScript.enabled = false;
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(expl, 3f);

    }
}
