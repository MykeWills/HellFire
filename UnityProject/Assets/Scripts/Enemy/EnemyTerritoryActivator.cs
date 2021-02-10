using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(BoxCollider))]
public class EnemyTerritoryActivator : MonoBehaviour {

    
    public List<Transform> enemies;

    void Start() {
        foreach (Transform child in transform) {
            enemies.Add(child);
        }
        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Target") {
            
            enemies.Clear();
            foreach (Transform child in transform) {
                enemies.Add(child);
            }
            for (int i = 0; i < enemies.Count; i++) {
                enemies[i].gameObject.SetActive(true);
                
            }
        }
    }

    //void OnTriggerExit(Collider col)
    //{
    //    if (col.tag == "Player")
    //    {
    //        enemies.Clear();
    //        foreach (Transform child in transform)
    //        {
    //            enemies.Add(child);
    //        }
    //        for (int i = 0; i < enemies.Count; i++)
    //        {
    //            enemies[i].gameObject.SetActive(false);
    //        }
    //    }
    //}
}
