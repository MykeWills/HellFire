using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    
    public GameObject explosion;

    void OnCollisionEnter()
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject); // destroy the grenade
        Destroy(expl, 6f); // delete the explosion after 3 seconds

    }
}
