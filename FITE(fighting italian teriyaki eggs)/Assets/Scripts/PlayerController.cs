using UnityEngine;
using UnityEngine.InputSystem;

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
    Animator animator;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>().x;
        animator.SetFloat("Speed", Mathf.Abs(move));
        switch (move)
        {
            case>0:
                spriteRenderer.flipX = true;
                break;
            case < 0:
                spriteRenderer.flipX = false;
                break;
        }
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
    public void Jump(InputAction.CallbackContext context)
    {
        print(jumpsRemaining);
        if (jumpsRemaining != 0 && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            jumpsRemaining--;
        }
    }
}
