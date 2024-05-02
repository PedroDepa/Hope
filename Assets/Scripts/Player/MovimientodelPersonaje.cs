
using UnityEngine;

public class MovimientodelPersonaje : MonoBehaviour

{
    [SerializeField] private Transform cam;


    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float turnSmoothTime = 0.1f; // Se ha ajustado el tiempo de suavizado de la rotación
    private bool canRun;

    float turnSmoothVelocity;
    float verticalVelocity;

    private Animator animator;




    void Start()
    {
        canRun = false;
        animator = GetComponent<Animator>();
        currentSpeed = walkSpeed;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("speed", direction.sqrMagnitude);
        if (direction.magnitude >= 0.1f)
        {
            canRun = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);


            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * currentSpeed * Time.deltaTime, Space.World);
        }
        Vector3 verticalMove = Vector3.up * verticalVelocity;
        transform.Translate(verticalMove * Time.deltaTime, Space.World);

        if (isRunning && canRun)
        {
            animator.SetBool("isRunning", true);
            currentSpeed = runSpeed;
        }
        else
        {
            canRun = false;
            animator.SetBool("isRunning", false);
            currentSpeed = walkSpeed;
        }

    }
}
