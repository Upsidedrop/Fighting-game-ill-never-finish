using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float move = 0;
    readonly float movementSpeed = 5.5f;
    Rigidbody2D rb;
    Controls controls;
    readonly float jumpStrength = 20;
    int jumpsRemaining;
    Collider2D playerCollider;
    bool preparingJump;
    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        controls.PlayerControls.MoveLeft.performed += ctx => move = -1;
        controls.PlayerControls.MoveLeft.canceled += ctx => move = 0;
        controls.PlayerControls.MoveRight.performed += ctx => move = 1;
        controls.PlayerControls.MoveRight.canceled += ctx => move = 0;
        controls.PlayerControls.Jump.performed += ctx => Jump();

    }
    private void OnEnable()
    {
        controls.PlayerControls.Enable();
    }
    private void OnDisable()
    {
        controls.PlayerControls.Disable();
    }
    private void Update()
    {
        if (Physics2D.Raycast(
            playerCollider.bounds.center,
            Vector2.down,
            playerCollider.bounds.extents.y + 0.05f) && !preparingJump)
        {
            jumpsRemaining = 2;
            preparingJump = true;
        }
        if (preparingJump && !Physics2D.Raycast(playerCollider.bounds.center,
                                                Vector2.down,
                                                playerCollider.bounds.extents.y + 0.05f))
        {
            preparingJump = false;
        }
        rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);
    }
    void Jump()
    {
        print(jumpsRemaining);
        if (jumpsRemaining != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            jumpsRemaining--;
        }
    }
}
