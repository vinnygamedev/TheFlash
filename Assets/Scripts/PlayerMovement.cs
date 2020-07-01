using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Regular Character Variables")]
    public Animator anim;
    public CharacterController controller;
    public float speed = 6;
    public float turnSpeed = 50f;

    [Header("Jump & Grounding Variables")]
    public float jumpHeight;
    public float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    Vector3 velocity;

    [Header("Speed Mode Variables")]
    public float superSpeed = 15;
    public float animSpeed;
    public GameObject lightningObj;
    public GameObject speedModeAura;
    public bool inSpeedMode = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        lightningObj.SetActive(false);
        speedModeAura.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);
        controller.Move(move * Time.deltaTime * speed);
        anim.SetFloat("Speed", move.magnitude);

        //Gravity - Jumping
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Speed Mode Aura
        if (inSpeedMode)
        {
            speedModeAura.SetActive(true);
        }
        else
        {
            speedModeAura.SetActive(false);
        }

        //If Moving, set the Speed Mode Run Trail On
        if (move.magnitude > 0)
        {
            Quaternion newDirection = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
            
            if(inSpeedMode)
            {
                lightningObj.SetActive(true);

                //Change Jump Height
                jumpHeight = 20f;
            }
        }
        else
        {
            lightningObj.SetActive(false);
            jumpHeight = 5;
        }

        //Activating Speed Mode
        if(Input.GetKeyDown(KeyCode.F) && inSpeedMode == false)
        {
            inSpeedMode = true;
            speed = superSpeed;
            anim.speed = animSpeed;
        }
        else if(Input.GetKeyDown(KeyCode.F) && inSpeedMode == true)
        {
            lightningObj.SetActive(false);
            inSpeedMode = false;
            speed = 5;
            anim.speed = 1f;
        }
    }
}
