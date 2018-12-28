using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossumController : MonoBehaviour {

    //Publics variables
    public float speed = 2;
    public float maxSpeed = 7;
    public float lengthOfTheGroundDetection = 2;
    public float lengthOfTheWallDetection = 2;


    // REFERENCIES
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRender;

    public Transform RightGroundDetection;
    public Transform LeftGroundDetection;
    public Transform RightWallDetection;
    public Transform LeftWallDetection;
    public Transform GroundCheck;


    //Private variables
    private bool facingRight = false;
    private int dir = -1;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movement();
	}


    private void movement()
    {
        dir = checkingEdges(dir);
        facingRight = dir > 0 ? true : false;
        flip(facingRight);
        rb2d.AddForce(new Vector2(dir, 0) * speed);
    }

    private int checkingEdges(int ant)
    {
        int direction = 0;

        // Checking if we are close to the edge or close to the wall, if so, then changing direction, if not then we are choosing random direction
        RaycastHit2D RightGroundInfo = Physics2D.Raycast(RightGroundDetection.position, Vector2.down, lengthOfTheGroundDetection);
        RaycastHit2D RightWallInfo = Physics2D.Raycast(RightWallDetection.position, Vector2.right, lengthOfTheWallDetection);
        if (RightGroundInfo.collider == false || RightWallInfo.collider == true)
            direction = -1;
        RaycastHit2D LeftGroundInfo = Physics2D.Raycast(LeftGroundDetection.position, Vector2.down, lengthOfTheGroundDetection);
        RaycastHit2D LeftWallInfo = Physics2D.Raycast(LeftWallDetection.position, Vector2.left, lengthOfTheWallDetection);
        if (LeftGroundInfo.collider == false || LeftWallInfo.collider == true)
            direction = 1;
        if(direction == 0)
            return ant;
        return direction;
        
    }

    



    // Flip the character
    private void flip(bool left)
    {
        spriteRender.flipX = left;
    }



    public void die()
    {
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullets")
        {
            die();
        }
    }


    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.15f, groundLayer);
    }
}
