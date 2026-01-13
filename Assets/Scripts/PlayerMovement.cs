using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private InputSystem_Actions playerInput;

    public static PlayerMovement Instance;
    public LayerMask groundLayer;

    public float playerSpeed = 8f;
    // public float jumpForce = 5f;
    private bool groundCheck;
    // private bool jumpPerformed;

    // public Transform cameraTransform; // Assign this in the Inspector

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        Instance = this;
        playerInput = new InputSystem_Actions();
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
        // playerInput.Player.Jump.performed += OnJumpPerformed;
    }

    void OnDisable()
    {
        // playerInput.Player.Jump.performed -= OnJumpPerformed;
        playerInput.Player.Disable();
    }

    void OnDestroy()
    {
        // playerInput.Player.Jump.performed -= OnJumpPerformed;
        playerInput.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        groundCheck = Physics.Raycast(transform.position, Vector3.down, 7f, groundLayer);
        Debug.Log(groundCheck);

        if (rb != null)
        {
            Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();

            Vector3 moveDirection = Vector3.zero;
            // if (cameraTransform != null)
            // {
            //     Vector3 forward = cameraTransform.forward;
            //     Vector3 right = cameraTransform.right;

            //     // Ignore vertical component
            //     forward.y = 0f;
            //     right.y = 0f;
            //     forward.Normalize();
            //     right.Normalize();

            //     moveDirection = (right * movementInput.x + forward * movementInput.y).normalized;
            // }
            // else
            // {
            moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
            // }

            if (playerInput.Player.Move.IsPressed())
            {
                Vector3 movement = moveDirection * playerSpeed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
            }

            // if (groundCheck && jumpPerformed)
            // {
            //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //     jumpPerformed = false;
            // }

            // if (groundCheck && playerInput.Player.Sprint.IsPressed())
            // {
            //     playerSpeed = 12f;
            // }
            // else
            // {
            //     playerSpeed = 8f;
            // }
        }
    }

    // void OnJumpPerformed(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Jumped");
    //     jumpPerformed = true;
    // }
}
