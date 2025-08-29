using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;
    bool isFacingRight = true;

    [Header("Jump Settings")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    /*
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    */

    [Header("Gravity Settings")]
    [SerializeField] private float baseGravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float fallSpeedMult;

    [Header("Platform Settings")]
    [SerializeField] private Rigidbody2D platform;
    private bool plataformMoving = false;
   
    [Header("Life Settings")]
    public int vida = 5;

    [Header("Animation Settings")]
    public Animator animator;

    //Variaveis de Controle
    private bool isMoving = false;
    private bool isRunning = false;
    private bool ableToClimb = false;
    private bool isClimbing = false;
    private bool isGrounded = false;
    //private bool doubleJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Gravity();
        Movement();
        Jumping();
        Climbing();

        if (plataformMoving)
        {
            platform.velocity = new Vector2(0, 2);
        }


        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            FlipSprite();
        }

        if (Math.Abs(horizontalInput) > 0)
        {
            isMoving = true;

        }
        else
        {
            isMoving = false;
            isRunning = false;
        }

        if (ableToClimb && Math.Abs(verticalInput) > 0f)
        {
            isClimbing = true;
        }

        if(vida == 0)
        {
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Morreu();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Passou(0);
        }


        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);
        if (!isClimbing)
        {
            animator.SetFloat("yVelocity", rb.velocity.y);
        }
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isClimbing", isClimbing);
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls; 
    }

    void Movement()
    {
       if (!Input.GetKey(KeyCode.LeftShift)) { 
            rb.velocity = new Vector2((horizontalInput * moveSpeed) * 1.003f, rb.velocity.y);
            isRunning = false;
        }
        else
        {
            rb.velocity = new Vector2((horizontalInput * (moveSpeed + 5)) * 1.003f, rb.velocity.y);
            isRunning = true;
        }
    }

    void Jumping()  
    {
        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        */

        if(coyoteTimeCounter >0f && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 1.003f);
        }
        
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

    /*
        if (coyoteTimeCounter > 0f && !Input.GetKeyDown(KeyCode.Space))
        {
            doubleJump = false; 
        }

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (coyoteTimeCounter > 0f || doubleJump )
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * 1.003f);
                isGrounded = false;
                coyoteTimeCounter = 0f;
                doubleJump = !doubleJump;
            }
           
        }
    */
    }

    void Climbing()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            animator.speed = Math.Abs(verticalInput);
            rb.velocity = new Vector2(rb.velocity.x,verticalInput*moveSpeed);

        }
        else { 
            rb.gravityScale = 1f;
        }
      
    }

    void Gravity() {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMult;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y * 1.003f, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        if (collision.gameObject.name == "Penhasco")
        {
            vida=0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<HeartSystem>().vida = 0;
        }

        if (collision.gameObject.name == "Moving Platform")
        {
           plataformMoving=true;
        }

        if (collision.gameObject.name == "ParedeINV2")
        {
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Passou(2);
        }
        if (collision.gameObject.name == "Fim")
        {
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Passou(0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Stairs")
        {
            ableToClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {   isGrounded = false;
        if (collision.gameObject.name == "Stairs")
        {
            ableToClimb = false;
            isClimbing = false;
        }
    }

}
