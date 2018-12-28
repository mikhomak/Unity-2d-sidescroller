using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {

    //Publics variables
    public float jumpForce= 150;
    public float speed = 2;
    public float maxSpeed = 7;
    public float timeToTheNextJump = 4;
    public float lengthOfTheGroundDetection;

    // REFERENCIES
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRender;
    private Animator animator;
    public Transform RightGroundDetection;
    public Transform LeftGroundDetection;
    public Transform GroundCheck;

    //Private variables
    private bool facingRight=false;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    // The frog will jump in the random period of time
    // Setting the values of the velocity and the state of the frog(standing or jumpin) to the animator
	void Update () {
        
        timeToTheNextJump += Time.deltaTime;
        if (timeToTheNextJump > 2) {
            moveHorizontal();
            jump();
            timeToTheNextJump = 0;
        }
        animator.SetBool("grounded", isGrounded());
        animator.SetFloat("vSpeed", rb2d.velocity.y);

    }



    public void moveHorizontal()
    {
        // Creation of the horizontal movement, randomizing the direction
        int rand = Random.Range(-1,2);
        while(rand == 0)
            rand = Random.Range(-1, 2);


        // If we are close to the edge then we change direction.
        // Checking the right and the left ground detection to see which edge we are facing
        RaycastHit2D RightGroundInfo = Physics2D.Raycast(RightGroundDetection.position, Vector2.down, lengthOfTheGroundDetection);
        if(RightGroundInfo.collider == false)
        {
            rand = -1;
        }
        RaycastHit2D LeftGroundInfo = Physics2D.Raycast(LeftGroundDetection.position, Vector2.down, lengthOfTheGroundDetection);
        if (LeftGroundInfo.collider == false)
        {
            rand = 1;
        }

        // Fliping the character and applying the force
        facingRight = rand > 0 ? true : false;
        flip(facingRight);
        rb2d.AddForce(new Vector2(rand,0)*speed);

    }



    // Flip the character
    private void flip(bool left)
    {
        spriteRender.flipX = left;
    }



    private void jump()
    {

        if(isGrounded())
            rb2d.AddForce(transform.up * jumpForce);
    }



    public void die()
    {
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullets")
        {
            die();
        }
    }



    // Checking if the character is standing on the ground
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.04f, groundLayer);
    }
}
