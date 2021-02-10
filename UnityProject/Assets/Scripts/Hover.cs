using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public float speed;
    public float AddAboveZero;
    public float UpAndDownAmount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPosition;
        newPosition = Vector3.zero;
        newPosition.y = (Mathf.Sin(Time.time * speed) + AddAboveZero) / UpAndDownAmount;

        transform.localPosition = newPosition;
    }
}
