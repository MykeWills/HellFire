using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour {

    public Transform Player;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Target"))
        {
            Player.transform.parent = gameObject.transform;
        }
        
    }
    void OnTriggerExit(Collider col)
    {
      
        Player.transform.parent = null;
    }
  
}
