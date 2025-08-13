using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController characterController;

    private Vector3 velocity;
    private Vector3 moveDirection;
    private Vector3 cameraForward;

    public float gravity = -9.81f;
    public float smoothTime = 0.1f;
    public float speed = 2f;
    public float runMultiplier = 2f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 currentInputDir = Vector3.zero;
    private float lastDirChangeTime = 0f;
    private float dirChangeDelay = 0.15f;

    // Doble salto
    public float jumpForce = 5f;
    private int jumpCount = 0;
    private int maxJumps = 2;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.3f;

    
    [Header("Animaciones que bloquean movimiento")]
    [SerializeField] private string[] animacionesQueBloqueanMovimiento;
    private bool movimientoBloqueado;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        
        movimientoBloqueado = false;
        foreach (string animName in animacionesQueBloqueanMovimiento)
        {
            if (stateInfo.IsName(animName))
            {
                movimientoBloqueado = true;
                break;
            }
        }

        
        if (movimientoBloqueado)
        {
            isGrounded = characterController.isGrounded;
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
            return;
        }

        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        
        if (Vector3.Angle(inputDir, currentInputDir) > 45f)
        {
            if (Time.time - lastDirChangeTime > dirChangeDelay)
            {
                currentInputDir = inputDir;
                lastDirChangeTime = Time.time;
            }
            else
            {
                inputDir = currentInputDir;
            }
        }
        else
        {
            currentInputDir = inputDir;
            lastDirChangeTime = Time.time;
        }

        
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        playerAnimator.SetBool("IsCrouching", isCrouching);

        
        Vector3 horizontalMove = Vector3.zero;
        if (currentInputDir.magnitude >= 0.1f)
        {
            cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 right = Camera.main.transform.right;
            right.y = 0;
            right.Normalize();

            moveDirection = currentInputDir.z * cameraForward + currentInputDir.x * right;

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            float finalSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift))
                finalSpeed *= runMultiplier;

            horizontalMove = moveDirection * finalSpeed;
        }

        
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            velocity.y = jumpForce;
            jumpCount++;
            playerAnimator.SetTrigger("Jump");
        }

        
        velocity.y += gravity * Time.deltaTime;

        
        Vector3 totalMove = horizontalMove + new Vector3(0, velocity.y, 0);
        characterController.Move(totalMove * Time.deltaTime);

        
        float animSpeed = currentInputDir.magnitude;
        if (Input.GetKey(KeyCode.LeftShift)) animSpeed *= runMultiplier;

        playerAnimator.SetFloat("VerticalSpeed", velocity.y);
        playerAnimator.SetBool("IsGrounded", isGrounded);
        playerAnimator.SetFloat("Speed", animSpeed, 0.15f, Time.deltaTime);
        playerAnimator.SetFloat("Direction", horizontal, 0.15f, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A) && stateInfo.IsName("Idle"))
            playerAnimator.SetTrigger("Left");

        if (Input.GetKeyDown(KeyCode.D) && stateInfo.IsName("Idle"))
            playerAnimator.SetTrigger("Right");
    }
}
