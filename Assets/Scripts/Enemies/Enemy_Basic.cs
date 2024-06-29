using UnityEngine;

public class Enemy_Basic : MonoBehaviour, Damage
{
    //Basic data for enemies
    [SerializeField] private float currentHealth;

    // Enemies taking damage from player
    private float hitPoints;

    // Internal variables
    private Rigidbody2D rb;
    private bool isGrounded;

    // Ground check variables
    public UnityEngine.Transform groundCheck;
    public float checkRadius = 0.1f; // Default value to ensure it's set
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 3f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Ground Check
        bool previousGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        OnDrawGizmos();
    }
    void OnDrawGizmos()
    {
        // Draw ground check sphere in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    //Taking damge from player
    public void takeDamage(float hitPoints)
    {
        currentHealth -= hitPoints;
        if (currentHealth <= 0)
        {
            defeat();
        }
    }

    //Attacking the player
    public void attackPlayer()
    {

    }

    //Upon Defeat
    public void defeat()
    {
        Destroy(gameObject);
    }
}
