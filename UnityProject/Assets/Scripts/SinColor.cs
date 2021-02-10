using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinColor : MonoBehaviour {
    float speed = 3.0f;

    float timeStamp;

    void Start()
    {
     
    }

    void Update()
    {

        Color newColor;

        newColor.r = (Mathf.Sin(Time.time * speed) + 1.5f) / 3;
        newColor.g = (Mathf.Sin(Time.time * speed) + 1.5f) / 3;
        newColor.b = (Mathf.Sin(Time.time * speed) + 1.5f) / 3;
        newColor.a = (Mathf.Sin(Time.time * speed) + 1.5f) / 3;

        GetComponent<Renderer>().material.color = newColor;


    }
}