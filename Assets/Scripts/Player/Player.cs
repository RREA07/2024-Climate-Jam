using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    // Movement and jump settings adjustable in the inspector
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;
    public float handling = 0.1f;

    // Health and taking damage
    [SerializeField] private float playerHealth;
    public Image currentHealthBar;

    // Stored location for respawning
    private Vector3 savedPosition;
    private Vector3 checkPointPosition;

    // Attck and combates
    private RaycastHit2D[] hit;
    [SerializeField] private Transform attackTrans;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attackPower;

    // Internal variables
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumped;
    private float moveInput;
    private ParticleSystem jumpDustInst;
    [SerializeField] private ParticleSystem jumpDust;
    private LogicManager logicManager;
    private SoundFXManager soundFXManager;
    private Animator ani;
    private float attackCoolDown = 0.4f;
    private float attackCoolDownCounter = 0f;
    private float iframe;
    public bool canMove;

    // Ground check variables
    public UnityEngine.Transform groundCheck;
    public float checkRadius = 0.1f; // Default value to ensure it's set
    public LayerMask whatIsGround;

    //Upgrades
    public bool dJump;
    private int dJumpCoolDown = 0;
    public bool canAttack;
    #endregion

    #region Updates
    void Start()
    {
        //Disable Game Over screen
        logicManager = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManager>();
        soundFXManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundFXManager>();
        logicManager.gameOverScreen.SetActive(false);
        logicManager.gamePauseMenu.SetActive(false);

        // Makes cursor invisible
        Cursor.visible = false;

        // Get components 
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        //Enables double jump&attack
        dJump = false;
        canAttack = false;

        //Sets attack range and power
        attackRange = 1.2f;
        attackPower = 1.0f;

        //Sets starting health
        playerHealth = 4f;

        canMove = true;
    }

    void Update()
    {
        //Health
        currentHealthBar.fillAmount = playerHealth * 0.25f;
        // Get horizontal input (A/D keys or Left/Right arrow keys)
        moveInput = User_Input.instance.moveInput.x;

        //Movement
        move();
        jump();
        ani.SetBool("Jump", jumped);
        ani.SetBool("Run", moveInput != 0);
        if (User_Input.instance.controls.Attacking.Melee.WasPressedThisFrame() && attackCoolDownCounter >= attackCoolDown && canAttack && canMove)
        {
            attackCoolDownCounter = 0f;
            attack(attackPower);
        }

        attackCoolDownCounter += Time.deltaTime;

        if (iframe > 0)
        {
            iframe -= Time.deltaTime;
        }

        // Pausing and un-pausing the game
        if (User_Input.instance.controls.Pausing.Pause.WasPressedThisFrame())
        {
            logicManager.gamePaused();
        }

        // Check if the player is on the ground
        bool previousGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (!isGrounded)
        {
            ani.SetBool("InAir", true);
        }
        else
        {
            ani.SetBool("InAir", false);
        }

        //Resets doubleJump
        if (isGrounded && dJump)
        {
            dJumpCoolDown = 1;
        }
        jumped = false;
    }
    #endregion

    #region Movements
    //Makes player move left and right
    void move()
    {
        // Horizontal movement
        if (canMove)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

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
        if (User_Input.instance.controls.Jumping.Jump.WasPressedThisFrame() && canMove)
        {
            if (isGrounded || dJumpCoolDown > 0)
            {
                dJumpCoolDown--;
                rb.velocity = new Vector2(rb.velocity.x * moveSpeed * Time.deltaTime, jumpForce);
                //Spawning double jump particles
                if (dJump && dJumpCoolDown == 0 && !isGrounded)
                {
                    jumpDustInst = Instantiate(jumpDust, transform.position, Quaternion.identity);
                    soundFXManager.playSFX(soundFXManager.doubleJump);
                }
                else
                {
                    soundFXManager.playSFX(soundFXManager.playerJump);
                }
                jumped = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Draw ground check sphere in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }


    private Transform GetTransform()
    {
        return transform;
    }
    #endregion
    #region Combat
    //Player attacks
    void attack(float hitPoints)
    {
        ani.SetTrigger("Attack");
        hit = Physics2D.CircleCastAll(attackTrans.position, attackRange, transform.right, 0f, attackableLayer);
        soundFXManager.playSFX(soundFXManager.playerAttack);
        for (int i = 0; i < hit.Length; i++)
        {
            Damage damage = hit[i].collider.gameObject.GetComponent<Damage>();
            if (damage != null)
            {
                damage.takeDamage(hitPoints);
            }
        }
    }
    //Visualizing attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTrans.position, attackRange);
    }

    //Falls off
    private void fallsOff()
    {
        takeDamage();
        transform.position = savedPosition;
    }

    //Take damage script
    public void takeDamage()
    {
        if (iframe <= 0)
        {
            ani.SetTrigger("Hurt");
            soundFXManager.playSFX(soundFXManager.playerLooseHealth);
            playerHealth--;
            if (playerHealth <= 0)
            {
                playerDies();
            }
        }
        iframe = 0.8f;
    }

    //When player health deplets
    public void playerDies()
    {
        canMove = false;
        logicManager.gameOver();
    }

    public void reloadPlayerStats()
    {
        transform.position = checkPointPosition;
        playerHealth = 4;
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "SafeZone")
        {
            // Saves location
            Debug.Log("Touched Save");
            savedPosition = collision.transform.position;
        }

        if (collision.transform.tag == "DamageZone")
        {
            // Falling off
            fallsOff();
        }

        if (collision.transform.tag == "CheckPoint")
        {
            checkPointPosition = collision.transform.position;
        }

        if (collision.tag == "NPC")
        {
            if (collision.GetComponent<DialogueTrigger>().encounters == 1)
            {
                canAttack = true;
                dJump = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            takeDamage();
        }
    }
}

