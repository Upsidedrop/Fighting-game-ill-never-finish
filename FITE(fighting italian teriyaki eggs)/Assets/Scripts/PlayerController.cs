using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float move = 0;
    readonly float movementSpeed = 5.5f;
    Rigidbody2D rb;
    readonly float jumpStrength = 20;
    int jumpsRemaining;
    Collider2D playerCollider;
    bool preparingJump;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    PlayerAttacks PlayerAttacks;
    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerAttacks = GetComponent<PlayerAttacks>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        PlayerAttacks.direction.y = context.ReadValue<Vector2>().y;
        move = context.ReadValue<Vector2>().x;
        animator.SetFloat("Speed", Mathf.Abs(move));
        switch (move)
        {
            case > 0:
                spriteRenderer.flipX = true;
                break;
            case < 0:
                spriteRenderer.flipX = false;
                break;
        }
        if (spriteRenderer.flipX)
        {

            PlayerAttacks.direction.x = 1;
        }
        else
        {
            PlayerAttacks.direction.x = -1;

        }
        if (context.canceled)
        {
            PlayerAttacks.direction.y = 0;
        }
    }

    private void Update()
    {
        CheckForGround();
        animator.SetBool("IsGrounded", isGrounded);
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
    void CheckForGround()
    {
        isGrounded = Physics2D.Raycast(
    playerCollider.bounds.center,
    Vector2.down,
    playerCollider.bounds.extents.y + 0.05f);
        if (isGrounded && !preparingJump)
        {
            jumpsRemaining = 2;
            preparingJump = true;
        }
        if (preparingJump && !isGrounded)
        {
            preparingJump = false;
        }
    }
}
