using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemyCollision : MonoBehaviour {

    [Header("Audio")]
    AudioSource audioSrc;
    public MeshRenderer FadeMaterial;
    public AudioClip EnemyHit;
    public AudioClip EnemyDead;
    public GameObject DemonBody;
    public float fallingSpeed;
    public float fadePerSecond;
    public GameObject Demon;
    public float EnemyHealth;
    public GameObject DemonObject;
    bool BFGFieldDamage;
    bool playDeath;
    // Use this for initialization
    void Start ()
    {
        playDeath = true;
        audioSrc = gameObject.GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {
        if (BFGFieldDamage)
        {
            EnemyHealth -= 1 * Time.deltaTime * 100.0f;
        }
        if (EnemyHealth <= 0)
        {
            EnemyHealth = 0;
            DemonBody.GetComponent<DemonEnemyController>().enabled = false;
            if (playDeath)
            {
                audioSrc.PlayOneShot(EnemyDead, 0.7f);
                playDeath = false;
            }
            
            var color = FadeMaterial.material.color;
            FadeMaterial.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            transform.Translate(Vector3.forward * -fallingSpeed * Time.deltaTime);
            if(transform.localPosition.y < -5)
            {
                Destroy(DemonObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BFGField"))
        {
            BFGFieldDamage = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BFGField"))
        {
            BFGFieldDamage = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            audioSrc.clip = EnemyHit;
            audioSrc.Play();
            EnemyHealth -= 1;

        }
        else if (other.gameObject.CompareTag("FlakBullet"))
        {
            Destroy(other.gameObject);
            audioSrc.clip = EnemyHit;
            audioSrc.Play();
            EnemyHealth -= 2;

        }
        else if (other.gameObject.CompareTag("PlasmaBullet"))
        {

            Destroy(other.gameObject);

            audioSrc.clip = EnemyHit;
            audioSrc.Play();
            EnemyHealth -= 5;
        }
       
        else if (other.gameObject.CompareTag("RailSlug"))
        {
            Destroy(other.gameObject);

            audioSrc.clip = EnemyHit;
            audioSrc.Play();
            EnemyHealth -= 25;
        }
        else if (other.gameObject.CompareTag("Rocket"))
        {
            Destroy(other.gameObject);

            audioSrc.clip = EnemyHit;
            audioSrc.Play();
            EnemyHealth -= 50;
        }
        if (other.gameObject.CompareTag("BFG"))
        {
            Destroy(other.gameObject);
            audioSrc.clip = EnemyHit;
            audioSrc.Play();
            EnemyHealth -= 100;
        }
    }
}
