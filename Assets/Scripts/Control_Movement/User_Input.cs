using UnityEngine;

public class User_Input : MonoBehaviour
{
    public static User_Input instance;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Player_Control controls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new Player_Control();
        controls.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }
}
