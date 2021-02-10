
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossRotation : MonoBehaviour
{
    public Transform target;
    public Transform center;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    public float degreesPerSecond = -65.0f;

    private Vector3 distance;
    

    // Use this for initialization
    void Start()
    {
        distance = transform.position - center.position;
        distance.x = DistanceX;
        distance.y = DistanceY;
        distance.z = DistanceZ;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RotateAroundPlayer()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
        transform.position = center.position + distance;
    }
}

