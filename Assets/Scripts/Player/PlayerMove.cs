using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMove : MonoBehaviour
{

    public CharacterController controller;

    [SerializeField] private float  walkSpeed = 5f;
    [SerializeField] private float  runSpeed = 10f;
    [SerializeField] private float  currentSpeed;
    [SerializeField] private float  turnSmoothTime = 0.5f;
    [SerializeField] private float gravity = -9.81f; // Gravedad

    float turnSmoothVelocity;
    float verticalVelocity;

    [SerializeField] private Transform cam;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        currentSpeed = walkSpeed;

    }
    
    void Update()
    {
        Move();

    }

    private void Move()
    {
      

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
    
       
        bool isRunning = Input.GetKey(KeyCode.LeftShift);


        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        animator.SetFloat("speed", direction.sqrMagnitude);
        if(direction.magnitude >= 0.1f)
        {
           
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0f,angle,0f);

            controller.Move(moveDir.normalized*currentSpeed*Time.deltaTime);

        }
            
            verticalVelocity += gravity * Time.deltaTime;
            Vector3 verticalMove = Vector3.up * verticalVelocity;
            controller.Move(verticalMove * Time.deltaTime);
         
         if (isRunning)
        {
            animator.SetBool("isRunning", true);

            currentSpeed = runSpeed;
        }
        else if (!isRunning)
        {
            animator.SetBool("isRunning", false);
        
            currentSpeed = walkSpeed;
        }

      
    }


 

}









        


