using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRadius : MonoBehaviour {

    public float dmgRadius;
    Vector3 PlayerPosition;

    GameObject Player;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(WaitForPlayer());
        if (Vector3.Magnitude(transform.position - PlayerPosition) < dmgRadius)
        {
            Player.gameObject.GetComponent<PlayerRespawn>().DoDamage(1 * Time.deltaTime * 20); ;
          
        }

    }
    IEnumerator WaitForPlayer()
    {
        Player = GameObject.Find("/Player/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/Player/");
            PlayerPosition = Player.gameObject.transform.position;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, dmgRadius);

       
    }
}
