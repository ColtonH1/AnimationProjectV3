using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //game objects
    public CharacterController controller; //player reference
    public Transform cam; //camera control
    public Transform groundcheck; //falling
    public LayerMask groundMask; //falling

    //moving
    public float speed = 6f;
    float startingSpeed;

    public float turnSmoothTime;
    private float turnSmoothVelocity;

    //falling
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    //jumping
    public float jumpHeight = 3f;

    Vector3 velocity; //falling
    bool isGrounded; //falling

    //animation
    public static bool isWalking;

    //debug check if grounded
    public float distToGround;

    //sound
    public AudioSource audioSource;
    //public AudioClip walking;
    bool isPlaying;

    //show floating gem text
    public GameObject gemWorthText;
    private FloatingGemWorthText changeGemWorthText;
    


    private void Start()
    {
        startingSpeed = speed; //set starting speed so when running, we can return back to normal
        audioSource = GetComponent<AudioSource>(); //get audio component
        isPlaying = false; //bool used to play audio without constant starting over
        changeGemWorthText = gemWorthText.GetComponent<FloatingGemWorthText>(); //in future implementations, we can add gems worth different amounts
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask); //check if we're grounded; used for jumping

        speed = Running(speed); //set current speed
        Moving();


        Jumping();

        //falling
        velocity.y = gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //if Spyro is walking and walking sound is not already being played
        if (isWalking && !isPlaying)
        {
            isPlaying = true;
            audioSource.Play();
            
        }
        //if Spyro stopped walking
        else if (!isWalking)
        {
            isPlaying = false;
            audioSource.Stop();
        }
    }

    //show floating text from Spyro and facing the camera
    public void ShowFloatingText()
    {
        changeGemWorthText.SetText("1");
        Instantiate(gemWorthText, transform.position, transform.rotation, transform);
    }

    private void Moving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            isWalking = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            isWalking = false;
        }
    }

    //set cursor when paused
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //debug check if grounded
    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, Vector3.down, distToGround + 2f))
        {
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("Not Grounded");
        }
    }

    //will get to work in future implementations; not needed right now
    private void Jumping()
    {
        //check if grounded to reset velocity when falling (or lack thereof)
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }

    //set how much faster to run
    private float Running(float currentSpeed)
    {
        //run while key is pressed
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed *= 3;
        }
        //return to normal speed when key is not pressed anymore
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = startingSpeed;
        }

        return currentSpeed;
    }
}
