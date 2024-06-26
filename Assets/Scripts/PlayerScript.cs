using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    private bool keyDown;
    private float speed = 9f;
    private bool inAir;
    [SerializeField] private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        inAir = false;
        isDead = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move(Vector2.right);
        }


    }

    void move(Vector2 direction)
    {
        if (!inAir && playerRb.velocity.magnitude < 10 && !isDead)
        {
            playerRb.AddForce(direction * speed);
        }
    }

    void attack()
    {

    }

    void jump()
    {

    }

}
