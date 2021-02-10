using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour {

    public float RotateOnX;
    public float RotateOnY;
    public float RotateOnZ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(RotateOnX, RotateOnY, RotateOnZ) * Time.deltaTime);

    }
}
