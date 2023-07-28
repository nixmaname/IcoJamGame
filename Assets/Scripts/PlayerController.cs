using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravityInc = 4;
    public float wallGravity = 0.5f;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck, leftCheck, rightCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float coyoteTime = 0.1f; // Adjust this value for the grace period

    private Rigidbody2D rb;
    public bool isGrounded = false;
    public bool leftWall = false;
    public bool rightWall = false;
    private float coyoteTimer = 0f;
    private float regularGravity;
    public bool cannotMove = false;
    private Vector2 jumpTo;
    private BoxCollider2D col;

    public TextMeshProUGUI text;
    float timerDisplay = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityInc; // Adjust the gravity to your preference
        regularGravity = gravityInc;
        col = GetComponent<BoxCollider2D>();

        // If groundCheck is not assigned, use the player's transform as the groundCheck
        if (groundCheck == null)
            groundCheck = transform;
    }

    private void Update()
    {
        if (timerDisplay > 0)
        {
            timerDisplay -= Time.deltaTime;
        }
        else
        {
            timerDisplay = 3f;
        }

        text.text = timerDisplay.ToString("F1");
        
        // Check if the player is on the ground or in the grace period
        leftWall = Physics2D.OverlapCircle(leftCheck.position, groundCheckRadius/4, wallLayer);
        rightWall = Physics2D.OverlapCircle(rightCheck.position, groundCheckRadius/4, wallLayer);


        if (rb.velocity.y <= 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            col.isTrigger = false;
        }
        else
        {
            isGrounded = false;
            col.isTrigger = true;
        }

        if(leftWall || rightWall)
        {
            if (col.isTrigger)
            {
                col.isTrigger = false;
            }
        }

        if ((!leftWall && !rightWall) && rb.gravityScale != regularGravity)
        {
            rb.gravityScale = regularGravity;
        }
        if (isGrounded)
        {
            if (rb.gravityScale != regularGravity)
                rb.gravityScale = regularGravity;

            coyoteTimer = coyoteTime;
            if (cannotMove)
                cannotMove = false;

            if (leftWall || rightWall)
            {
                leftWall = false;
                rightWall = false;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            if ((leftWall || rightWall) && rb.velocity.y < 0)
            {
                rb.gravityScale = wallGravity;
                rb.velocity = Vector2.zero;
            }
        }

        // Handle horizontal movement
        if ((!leftWall && !rightWall) && !cannotMove)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        }
        if (cannotMove)
        {
            rb.velocity = new Vector2(jumpTo.x * moveSpeed, rb.velocity.y);
            if (rb.velocity.y < -1.5f)
            {
                cannotMove = false;
            }
        }


        // Handle jumping with the grace period
        if (isGrounded || coyoteTimer > 0f || leftWall || rightWall)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (leftWall)
                {
                    rb.velocity = new Vector2(moveSpeed, jumpForce);
                    jumpTo = new Vector2(1, 0);
                    cannotMove = true;
                }
                else if (rightWall)
                {
                    rb.velocity = new Vector2(-moveSpeed, jumpForce);
                    jumpTo = new Vector2(-1, 0);
                    cannotMove = true;
                }
                else
                {

                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    coyoteTimer = 0f; // Reset the timer after the jump
                }
            }
        }

        
    }
}
