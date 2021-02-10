using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterTrigger : MonoBehaviour {


    public AudioSource audioSrc;
    SphereCollider EnemyRangeCollider;

    private void Start()
    {
        EnemyRangeCollider = gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            audioSrc.Play();
            EnemyRangeCollider.enabled = false;
        }
    }




}
