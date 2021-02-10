using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{

    public Transform center;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    private Vector3 distance;
    public float degreesPerSecond = -65.0f;

    //private Vector3 v;

    void Start()
    {
        distance = transform.position - center.position;

        distance.x = DistanceX;
        //distance.y DistanceY;
        distance.z = DistanceZ;
    }

    void Update()
    {
       

        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;

        transform.position = center.position + distance;
    }
}