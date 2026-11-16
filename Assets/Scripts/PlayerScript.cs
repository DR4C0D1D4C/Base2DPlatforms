using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput playerInput;

    [SerializeField] float movementForce = 10.0f;
    [SerializeField] float jumpForce = 5.0f;

    Vector2 movementInput;
    bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        Physics2D.queriesStartInColliders = false;

        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        //movementInput = Input.GetAxis("Horizontal");
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    void Jump()
    {
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null && hit.distance < 0.48f && rb.linearVelocityY < 0.01f)
        {
            isGrounded = true;
        }

        rb.AddForce(Vector2.right * movementInput.x * movementForce);
    }
}
