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
    bool waitForGround,hasflipped;
    bool facingRight = true;
    SpriteRenderer sr;
    

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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

        if((rb.velocity.x > 0 && !facingRight) || (rb.velocity.x < 0 && facingRight))
        {
            Flip();
        }

        //text.text = timerDisplay.ToString("F1");

        // Check if the player is on the ground or in the grace period
        leftWall = Physics2D.OverlapCircle(leftCheck.position, groundCheckRadius / 4, wallLayer);
        rightWall = Physics2D.OverlapCircle(rightCheck.position, groundCheckRadius / 4, wallLayer);


        if (rb.velocity.y <= 0)
        {
            //Ako otivame nadolu imame collider i mojem da checkvame grounda
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            col.isTrigger = false;
        }
        else
        {
            //Ako otivame nagore nqmame collider
            isGrounded = false;
            col.isTrigger = true;
        }

        if (leftWall || rightWall)
        {
            if (col.isTrigger)
            {
                col.isTrigger = false;
            }
        }

        if ((!leftWall && !rightWall) && rb.gravityScale != regularGravity)
        {
            //Ako ne sme zalepnali na stenata - opravqme gravitaciqta
            rb.gravityScale = regularGravity;
        }
        if (isGrounded)
        {
            if (waitForGround)
            {
                ResetRecorder();
            }

            if (rb.gravityScale != regularGravity)
                rb.gravityScale = regularGravity;

            coyoteTimer = coyoteTime;
            if (cannotMove)
                cannotMove = false;

            if (leftWall || rightWall)
            {
                //Ako sme na zemqta i sme do stenata - ne vzimame pod vnimanie stenite
                leftWall = false;
                rightWall = false;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            if ((leftWall || rightWall) && rb.velocity.y < 0)
            {
                if (!hasflipped)
                {
                    Flip();
                    hasflipped = true;
                }
                //Ako padame nadolu i sme dokosnati do stena napravi gravitaciqta niska vse edno se plyzgame bavno
                rb.gravityScale = wallGravity;
                rb.velocity = Vector2.zero;
            }
        }

        // Handle horizontal movement
        if ((!leftWall && !rightWall) && !cannotMove)
        {
            //Ne mojem da hodim ako sme na stena
            //Ne mojem da hodim ako sme bili na stena, skochili sme i y velocitito e positivno
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        }
        if (cannotMove)
        {
            //Ako tykmo sme skochili ot stena dokato velocitito e positivno skachame prinuditelno kym protivopolojnata posoka
            //Vmesto pozitivno y, za sega e nad -1.5f za da ne moje da se katerish po stena
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
                    hasflipped = false;
                }
                else if (rightWall)
                {
                    rb.velocity = new Vector2(-moveSpeed, jumpForce);
                    jumpTo = new Vector2(-1, 0);
                    cannotMove = true;
                    hasflipped = false;
                }
                else
                {

                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    coyoteTimer = 0f; // Reset the timer after the jump
                }
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            //Skachame
            waitForGround = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimer = 0f;
            //other.transform.position = transform.position;

            //skrivame duhcheto
            transform.GetComponent<Recorder>().cc = other.transform.GetComponent<CopyCat>();
            other.gameObject.SetActive(false);
        }
        
    }

    void ResetRecorder()
    {
        //Zapochvame zapis na pozicii
        Recorder rec;
        rec = transform.GetComponent<Recorder>();

        //Nulirame vsichko
        rec.timer = 0;
        rec.oldpos = transform.position;
        rec.canPress = true;
        rec.canRecord = true;
        rec.cc.gameObject.transform.position = transform.position;
        waitForGround = false;
    }

    void Flip()
    {
        sr.flipX = !sr.flipX;

        facingRight = !facingRight;
    }
}
