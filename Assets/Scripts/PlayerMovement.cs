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

            moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            if (playerInput.Player.Move.IsPressed())
            {
                Vector3 movement = moveDirection * playerSpeed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
            }
        }
    }
}
