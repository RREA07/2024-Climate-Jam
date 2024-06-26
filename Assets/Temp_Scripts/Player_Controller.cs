using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player_Controller_Placeholder : MonoBehaviour
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
    public float checkRadius = 0.1f; // Default value to ensure it's set
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

        // Check if the player is on the ground
        bool previousGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded != previousGrounded)
        {
            UnityEngine.Debug.Log("Is Grounded: " + isGrounded);
        }

        // Check for jump input (space bar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Space key pressed");
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Attempting to jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            UnityEngine.Debug.Log("Jump triggered with velocity: " + rb.velocity);
        }
    }

    void FixedUpdate()
    {
        // Horizontal movement
        float moveVelocity = moveInput * moveSpeed;
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

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
