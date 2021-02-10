using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MykesHeadBobbing : MonoBehaviour {

    private float timer = 0.0f;

    public static float bobbingSpeed;
    public static float bobbingAmount;

    public float bobbingRunSpeed;
    public float bobbingRunAmount;

    public float bobbingWalkSpeed;
    public float bobbingWalkAmount;

    float midpoint = -0.08f;
    bool isGrounded;
    bool isRunning;
   
    bool isMoving;
   

    private void FixedUpdate()
    {
        isGrounded = PlayerController.grounded;
     
        isRunning = PlayerController.isRunning;
        isMoving = PlayerController.isMoving;
      
    }

    void Update()
    {

       

        float waveslice = 1.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

     

        if (isRunning)
        {
            //bobbingRunSpeed = 0.3f;
            bobbingSpeed = bobbingRunSpeed;
            //bobbingRunAmount = 0.4f;
            bobbingAmount = bobbingRunAmount;
        }
        else
        {
            //bobbingWalkSpeed = 0.2f;
            bobbingSpeed = bobbingWalkSpeed;
            //bobbingWalkAmount = 0.2f;
            bobbingSpeed = bobbingWalkAmount;

        }

        if (isMoving && isGrounded)
        {
            Vector3 cSharpConversion = transform.localPosition;


            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }
            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                cSharpConversion.y = midpoint + translateChange;
            }
            else
            {
                cSharpConversion.y = midpoint;
            }

            transform.localPosition = cSharpConversion;
        }
    }
}