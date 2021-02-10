using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]


public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float runSpeed = 11.0f;
    public bool limitDiagonalSpeed = true;
    public bool toggleRun = false;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float fallingDamageThreshold = 10.0f;
    public bool slideWhenOverSlopeLimit = false;
    public bool slideOnTaggedObjects = false;
    public float slideSpeed = 12.0f;
    public bool airControl = false;
    public float antiBumpFactor = .75f;
    public int antiBunnyHopFactor = 1;
    private Vector3 moveDirection = Vector3.zero;
    public static bool grounded = false;
    private CharacterController controller;
    private Transform myTransform;
    public float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private int jumpTimer;

    AudioSource audioSrc;
    public AudioClip Jump;
    public AudioClip footStep;
    public AudioClip land;
 

    public bool canRun;
    public static bool isRunning = false;
    public static bool isJumping = false;

    Rigidbody rb;
    public static bool isMoving;
    public bool isGrounded;
    public bool IsRunning;

    void Awake()
    {
        grounded = true;
       
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
    private void Start()
    {
      
        controller = GetComponent<CharacterController>();
        myTransform = transform;
        speed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
       
        canRun = true;
        isRunning = true;
        isGrounded = true;
      
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
     
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (rb.velocity.magnitude >= 0 && audioSrc.isPlaying == false && isMoving)
        {

            if (isRunning)
            {
                audioSrc.volume = Random.Range(0.8f, 1.0f);
                audioSrc.pitch = Random.Range(0.8f, 1.2f);
            }
            else
            {
                audioSrc.volume = Random.Range(0.6f, 0.8f);
                audioSrc.pitch = Random.Range(0.4f, 0.6f);
            }
            audioSrc.clip = footStep;
            audioSrc.Play();
        }

      
    

        if (rb.velocity.magnitude >= 0 && audioSrc.isPlaying == false && isMoving)
        {
            audioSrc.volume = Random.Range(0.6f, 0.8f);
            audioSrc.pitch = Random.Range(0.4f, 0.6f);
            audioSrc.clip = footStep;
            audioSrc.Play();
        }

    }
    void Update()
    {
        if (canRun && isMoving && grounded && Input.GetButton("Run"))
        {
            speed = runSpeed;
            isRunning = true;
        }
        else
        {
            speed = walkSpeed;
            isRunning = false;
        }
        isGrounded = grounded;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
        if (isJumping)
        {

            audioSrc.clip = land;
            audioSrc.volume = Random.Range(0.3f, 0.5f);
            audioSrc.pitch = Random.Range(0.6f, 1.1f);
            audioSrc.Play();
        }
        else if (!isGrounded)
        {
            audioSrc.clip = land;
            audioSrc.volume = Random.Range(0.3f, 0.5f);
            audioSrc.pitch = Random.Range(0.6f, 1.1f);
            audioSrc.Play();
        }
        isJumping = false;

        contactPoint = hit.point;

    }
    void FallingDamageAlert(float fallDistance)
    {
        print("Ouch! Fell " + fallDistance + " units!");
        if (fallDistance > 150)
        {
           
        }
    }

    public void MovePlayer()
    {
       
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;


        if (grounded)
        {
            bool sliding = false;
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }

            else
            {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }

            if (falling)
            {
                falling = false;
                if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                    FallingDamageAlert(fallStartLevel - myTransform.position.y);
            }


            if ((sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide"))
            {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }

            else
            {
                moveDirection = new Vector3(inputX, -antiBumpFactor, inputY);
                moveDirection = myTransform.TransformDirection(moveDirection) * speed;
                playerControl = true;
            }

            if (!Input.GetButton("Jump"))
                jumpTimer++;
            else if (jumpTimer >= antiBunnyHopFactor)
            {
                isJumping = true;
              
                audioSrc.clip = Jump;
                audioSrc.volume = Random.Range(0.8f, 1.0f);
                audioSrc.pitch = Random.Range(0.8f, 1.0f);
                audioSrc.PlayOneShot(Jump);
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
            }
        }
        else
        {

            if (!falling)
            {
                falling = true;
                fallStartLevel = myTransform.position.y;
            }

            if (airControl && playerControl)
            {

                moveDirection.x = inputX * speed * inputModifyFactor;
                moveDirection.z = inputY * speed * inputModifyFactor;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }



  
}
