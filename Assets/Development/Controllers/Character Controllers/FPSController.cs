using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FPSController : MonoBehaviour
{

    private Rigidbody rb;
    private Rigidbody Rb { get { return (rb == null) ? rb = GetComponent<Rigidbody>(): rb; } }

    private CapsuleCollider capsuleCollider;
    private CapsuleCollider CapsuleCollider { get { return (capsuleCollider == null) ? capsuleCollider = GetComponent<CapsuleCollider>() : capsuleCollider; } }

    private CharacterSettings characterSettings;
    private CharacterSettings CharacterSettings { get { return (characterSettings == null) ? characterSettings = GetComponent<CharacterSettings>() : characterSettings; } }

    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
    }

    void FixedUpdate()
    {
        CharacterSettings.IsGrounded = Physics.CheckCapsule(
            CapsuleCollider.bounds.center,
            new Vector3(CapsuleCollider.bounds.center.x, CapsuleCollider.bounds.min.y, CapsuleCollider.bounds.center.z),
            CapsuleCollider.radius * 0.9f,
            ~LayerMask.NameToLayer("Player")
        );

        Movement();
        Rotation();
        Jumping();
    }

    public void Movement()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);

        Vector3 movement = moveDirection * CharacterSettings.WalkingSpeed * Time.deltaTime;
        movement.y = Rb.velocity.y; // Preserve vertical velocity

        Rb.velocity = movement;
    }

    public void Rotation()
    {
        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * CharacterSettings.MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * CharacterSettings.MouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

    }

    public void Jumping()
    {
        // Jumping
        if (CharacterSettings.IsGrounded && Input.GetButtonDown("Jump"))
        {
            Rb.AddForce(Vector3.up * CharacterSettings.JumpForce, ForceMode.Impulse);
        }
    }
}
