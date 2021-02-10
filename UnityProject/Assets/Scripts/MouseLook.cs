using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]

public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 5f;
    public float sensitivityY = 5f;

    private float minimumX = 0f;
    private float maximumX = 360f;

    public float minimumY = -30f;
    public float maximumY = 45f;

    private float rotationY = 0f;

    public bool SmoothRotation;

    void Start()
    {
        
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
           
    }


    void Update ()
	{
        if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
            if (SmoothRotation)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX, 0);
            }
            else
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
        }
		else
		{
            if (SmoothRotation)
            {
                rotationY += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            }
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
    }
}