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
    public GameObject pointA;
    public GameObject pointB;
    private Animator animator;
    private Transform currentPoint;
    private SoundFXManager soundFXManager;
    public float speed = 2f;

    // Ground check variables
    public UnityEngine.Transform groundCheck;
    public float checkRadius = 0.1f; // Default value to ensure it's set
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 3f;
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        soundFXManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Ground Check
        bool previousGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //Path finding
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }

    //Taking damge from player
    public void takeDamage(float hitPoints)
    {
        currentHealth -= hitPoints;
        soundFXManager.playSFX(soundFXManager.enemyLooseHealth);
        Debug.Log("Got hit, current health is : " + currentHealth);
        if (currentHealth <= 0)
        {
            defeat();
        }
    }

    //Upon Defeat
    public void defeat()
    {
        soundFXManager.playSFX(soundFXManager.enemyDies);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
