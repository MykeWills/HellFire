using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DemonBossCollision : MonoBehaviour
{
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
    public Text BossHealthText;
    public static bool BossDead;
    public float awarenessRange;
    public Transform Target;

    // Use this for initialization
    void Start ()
    {
        BossDead = false;
        BossHealthText.text = "";
        playDeath = true;
        audioSrc = gameObject.GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update () {
        if (BFGFieldDamage)
        {
            EnemyHealth -= 1 * Time.deltaTime * 100.0f;
        }
        if (Vector3.Magnitude(transform.position - Target.transform.position) < awarenessRange)
        {
            SetCountHealth();
        }
            if (EnemyHealth <= 0)
        {
            EnemyHealth = 0;
            DemonBody.GetComponent<DemonBossController>().enabled = false;
            if (playDeath)
            {
                audioSrc.PlayOneShot(EnemyDead, 0.7f);
                playDeath = false;
            }
            BossHealthText.text = "";
            BossDead = true;
            var material = Demon.GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            transform.Translate(Vector3.forward * -fallingSpeed * Time.deltaTime);
            if (transform.localPosition.y < -5)
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
    void SetCountHealth()
    {
        BossHealthText.text = "Mega CacoDemon " + Mathf.CeilToInt(EnemyHealth).ToString();
    }
}
