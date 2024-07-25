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
    private Transform currentPoint;
    private SoundFXManager soundFXManager;
    public float speed = 2f;
    public float hitPause = 0f;
    private ParticleSystem hitDustInst;
    [SerializeField] private ParticleSystem hitDust;

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

        if (hitPause <= 0)
        {
            move();
        }

        if (hitPause >= 0)
        {
            hitPause -= Time.deltaTime;
        }
    }

    //Taking damge from player
    public void takeDamage(float hitPoints)
    {
        currentHealth -= hitPoints;
        soundFXManager.playSFX(soundFXManager.enemyLooseHealth);
        hitDustInst = Instantiate(hitDust, transform.position, Quaternion.identity);
        hitPause = 1.5f;
        if (currentHealth <= 0)
        {
            defeat();
        }
    }

    public void move()
    {
        //Path finding
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = Vector3.one;
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
