using UnityEngine;

public class Player_Controller_Placeholder : MonoBehaviour
{
    // Movement and jump settings adjustable in the inspector
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;
    public float handling = 0.1f;

    // Attck and combates
    private RaycastHit2D[] hit;
    [SerializeField] private Transform attackTrans;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attackPower;

    // Internal variables
    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private ParticleSystem jumpDustInst;
    [SerializeField] private ParticleSystem jumpDust;

    // Ground check variables
    public UnityEngine.Transform groundCheck;
    public float checkRadius = 0.1f; // Default value to ensure it's set
    public LayerMask whatIsGround;

    //Upgrades
    public bool dJump;
    private int dJumpCoolDown = 0;


    void Start()
    {
        // Get the Rigidbody2D component attached to the player GameObject
        rb = GetComponent<Rigidbody2D>();

        //Enables double jump
        dJump = true;

        //Sets attack range and power
        attackRange = 1.5f;
        attackPower = 1.0f;
    }

    void Update()
    {
        // Get horizontal input (A/D keys or Left/Right arrow keys)
        moveInput = User_Input.instance.moveInput.x;

        //Movement
        move();
        jump();
        attack(attackPower);

        // Check if the player is on the ground
        bool previousGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Resets doubleJump
        if (isGrounded && dJump)
        {
            dJumpCoolDown = 1;
        }
    }

    void OnDrawGizmos()
    {
        // Draw ground check sphere in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    //Makes player move left and right
    void move()
    {
        // Horizontal movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flips sprite
        if (moveInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    //Makes the player jumps into air
    void jump()
    {
        if (User_Input.instance.controls.Jumping.Jump.WasPressedThisFrame())
        {
            if (isGrounded || dJumpCoolDown > 0)
            {
                dJumpCoolDown--;
                rb.velocity = new Vector2(rb.velocity.x * moveSpeed * Time.deltaTime, jumpForce);
                //Spawning double jump particles
                if (dJump && dJumpCoolDown == 0 && !isGrounded)
                {
                    jumpDustInst = Instantiate(jumpDust, transform.position, Quaternion.identity);
                }
            }
        }

    }

    private Transform GetTransform()
    {
        return transform;
    }

    //Player attacks
    void attack(float hitPoints)
    {
        if (User_Input.instance.controls.Attacking.Melee.WasPressedThisFrame())
        {
            hit = Physics2D.CircleCastAll(attackTrans.position, attackRange, transform.right, 0f, attackableLayer);
            Debug.Log("Attacked");
            for (int i = 0; i < hit.Length; i++)
            {
                Damage damage = hit[i].collider.gameObject.GetComponent<Damage>();
                if (damage != null)
                {
                    damage.takeDamage(hitPoints);
                }
            }
        }
    }
    //Visualizing attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTrans.position, attackRange);
    }
}

