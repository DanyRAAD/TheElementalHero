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

    // Variables para suavizar dirección y evitar cambios bruscos
    private Vector3 currentInputDir = Vector3.zero;
    private float lastDirChangeTime = 0f;
    private float dirChangeDelay = 0.15f; // 150 ms

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Si el cambio es muy brusco y ocurrió muy rápido, mantener la dirección anterior
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

        // Mover solo si la magnitud es mayor que un umbral pequeño para evitar movimientos mínimos bruscos
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

            characterController.Move(moveDirection * finalSpeed * Time.deltaTime);
        }

        // Gravedad
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Animaciones: uso damping para suavizar los parámetros
        float animSpeed = currentInputDir.magnitude;
        if (Input.GetKey(KeyCode.LeftShift)) animSpeed *= runMultiplier;

        playerAnimator.SetFloat("Speed", animSpeed, 0.15f, Time.deltaTime);
        playerAnimator.SetFloat("Direction", horizontal, 0.15f, Time.deltaTime);

        var stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.A) && stateInfo.IsName("Idle"))
            playerAnimator.SetTrigger("Left");

        if (Input.GetKeyDown(KeyCode.D) && stateInfo.IsName("Idle"))
            playerAnimator.SetTrigger("Right");
    }
}

