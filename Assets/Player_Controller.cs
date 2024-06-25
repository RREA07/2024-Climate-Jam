using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // Movement and jump settings adjustable in the inspector
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float handling = 0.1f;

    // Internal variables
    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    // Ground check variables
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Camera follow variables
    public Transform cameraTransform;
    public Vector3 cameraOffset;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player GameObject
        rb = GetComponent<Rigidbody2D>();

        // Ensure the camera transform is assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Get horizontal input (A/D keys or Left/Right arrow keys)
        moveInput = Input.GetAxis("Horizontal");

        // Check for jump input (space bar)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void FixedUpdate()
    {
        // Horizontal movement
        float moveVelocity = moveInput * moveSpeed;
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Update camera position
        Vector3 targetPosition = transform.position + cameraOffset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, handling);
    }

    void OnDrawGizmos()
    {
        // Draw ground check sphere in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
