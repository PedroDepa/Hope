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
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float turnSmoothTime = 0.5f;
    [SerializeField] private float gravity = -9.81f; // Gravedad

    [SerializeField] private SphereCollider sphereCollider;
    public bool isMove = true;
    float turnSmoothVelocity;
    float verticalVelocity;

    [SerializeField] private Transform cam;

    private Animator animator;
    private bool isGrounded = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        if (isMove)
        {
            Move();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        isMove = true;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("speed", direction.sqrMagnitude);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * currentSpeed * Time.deltaTime, Space.World);
        }

        if (isGrounded)
        {
            verticalVelocity = 0f; // Resetea la velocidad vertical si está en el suelo
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 verticalMove = Vector3.up * verticalVelocity;
        transform.Translate(verticalMove * Time.deltaTime, Space.World);

        if (isRunning)
        {
            animator.SetBool("isRunning", true);
            currentSpeed = runSpeed;
        }
        else
        {
            animator.SetBool("isRunning", false);
            currentSpeed = walkSpeed;
        }
    }
}










        


