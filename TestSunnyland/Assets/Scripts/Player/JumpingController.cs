using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingController : MonoBehaviour
{


    //PUBLIC
    public float jumpForce = 120;
    public LayerMask groundLayer;
    public Transform GroundCheck;
    public float lengthOfTheWallDetection = 0.03f;
    public float gravityScaleOfTheTouchingTheWall = 0.001f;
    public Vector2 bouncingFromTheWall;

    // REFERENCES
    private Rigidbody2D rb2d;
    private Animator animator;
    private AudioSource audioJump;
    public Transform RightWallDetection;
    public Transform LeftWallDetection;

    // BOOLEANS
    private bool grounded;
    private bool RightWallTouching;
    private bool LeftWallTouching;
    private bool TouchingTheWall = false;
    private bool isItPossibleToJump;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioJump = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            
        isItPossibleToJump = CheckIfItsPossibleToJump();
        if (Input.GetButtonDown("Jump") && isItPossibleToJump)
        {
            jump();
        }
        jumpingAnimation();
    }



    // Checking if it's possible to jump
    // Requirement:
    // Touching the wall on the right side or the left side
    // Or if the character is grounded
    private bool CheckIfItsPossibleToJump()
    {
        RightWallTouching = CheckThetWall(RightWallDetection);
        // If we are touching one wall that's enough to work with
        // To not override our Gravity Scale
        if (!RightWallTouching)
        {
            LeftWallTouching = CheckThetWall(LeftWallDetection);
        }
            
        TouchingTheWall = RightWallTouching || LeftWallTouching;
        grounded = isGrounded();
        return grounded || TouchingTheWall;
    }



    private void jump()
    {
        audioJump.Play();
        // If we are toucing the wall we want to bounce to the other direction the wall is facing
        if (TouchingTheWall)
        {
            if (RightWallTouching)
            {
                Vector2 bouncingFromTheRightWall = new Vector2(-bouncingFromTheWall.x, bouncingFromTheWall.y);
                rb2d.AddForce(bouncingFromTheRightWall);
            }
            else if (LeftWallTouching)
            {
                rb2d.AddForce(bouncingFromTheWall);
            }
        }
        // Touching the wall or no we want to jump up
        else
        {
            rb2d.AddForce(transform.up * jumpForce);
        }
    }


    // Set to the animator values 
    private void jumpingAnimation()
    {
        animator.SetBool("grounded", isGrounded());
        animator.SetFloat("speedVer", rb2d.velocity.y);
    }



    private bool CheckThetWall(Transform wallToCheck)
    {
        bool result = Physics2D.OverlapCircle(wallToCheck.position, lengthOfTheWallDetection, groundLayer);
        if (result)
        {
            rb2d.gravityScale = gravityScaleOfTheTouchingTheWall;            
        }
        else
        {
            rb2d.gravityScale = 0.5f; // Initial value of the gravity scale
        }
        return result;
    }


    // Check if the characeter is on the ground
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.07f, groundLayer);
    }
}
        
