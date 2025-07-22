using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float verticalVelocity;
    public float groundCheckDistance = 0.2f;
    public Transform cameraTransform;
    public Animator animator;

    private CharacterController controller;
    private float rotationVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimiento de entrada
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Estado de caminar
        animator.SetBool("isWalking", direction.magnitude > 0.1f);

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Mantener pegado al suelo
        }

        // Movimiento en plano XZ
        Vector3 move = Vector3.zero;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            move = moveDir.normalized * speed;
        }

        // Aplicar gravedad
        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        // Mover al personaje
        controller.Move(move * Time.deltaTime);
    }
}
