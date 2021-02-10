using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    //public float speed = 3;
    //public float AddAboveZero = 1;
    //public float UpAndDownAmount = 5;
    public Vector3 rotations = new Vector3(0, 90, 0);
    //private Vector3 startLoc;
    // Use this for initialization
    void Start()
    {
        //startLoc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPosition;
        //newPosition = startLoc;
        //newPosition.y = startLoc.y + ((Mathf.Sin(Time.time * speed) + AddAboveZero) / UpAndDownAmount);
        transform.Rotate(new Vector3(rotations.x, rotations.y, rotations.z) * Time.deltaTime);
        //transform.localPosition = newPosition;

    }
}
