using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    
    //PUBLIC
    public float speed = 5;
    public float jumpForce = 2;
    public LayerMask groundLayer;
    


    // REFERENCES
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRender;
    private Animator animator;
    public GameObject bullet;
    public GameObject changingDirection;
    public Transform GroundCheck;



    // BOOLEANS
    // private bool grounded = true;
    private bool headingDirection = false;
    [HideInInspector]
    public bool gameOver = false;
    [HideInInspector]
    public bool pauseGame = false;

    // PRIVATE VARS
    private float maxSpeed = 7;
    private Vector3 positionToRespawn;
    
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameOver = false;
        rb2d.freezeRotation = true; // freeze rotation
    }
	



	// Update is called once per frame
	void Update () {
        // We only allow acces to inputs to user when the game is running
        if (GameManagerController.IsInputEnable)
        {
            InputAction();
        }
        
	}

    private void FixedUpdate()
    {   
        movement();
    }


    private void InputAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        // Pause the game 
        if (Input.GetButtonDown("Cancel"))
        {
            pauseGame = true;
        }
    }



    private void shoot()
    {
        Vector3 heading = new Vector3(0, 0, 1);
        if (headingDirection)
            heading.x = 1;
        else
            heading.x = -1;

       Instantiate(bullet, transform.position, Quaternion.Euler(heading));
    }


    


    // Running controller
    private void movement()
    {
        float movHor = Input.GetAxis("Horizontal");
        if (movHor * speed > maxSpeed)
            movHor = maxSpeed;



        // FLIP THE SPRITE
        if (movHor < 0) 
            flip(true, rb2d.velocity.x); // left
        else if (movHor > 0)
            flip(false, rb2d.velocity.x); //right

        runningAnimation(movHor);


        Vector2 movement = new Vector2(movHor, 0) * speed;

        rb2d.AddForce(movement);
    }


   


    // Running Animation
    private void runningAnimation(float movement)
    {
        animator.SetFloat("speedHor",Mathf.Abs(movement));
    }


    // Flip the character and instantiate the particle system of the changing direction
    private void flip(bool left, float speed)
    {
        if (headingDirection != left)
        {
            // Instantiatating the particle system only if we it's possible
            if (IfTheParticleSystemCanInstantiate(isGrounded(), speed))
            {
                // Which way should it be going
                Quaternion rotationOfPar = transform.rotation;
                if (!left)
                    rotationOfPar.y = -180;
                Vector3 positionOfTheParticles = new Vector3(transform.position.x, transform.position.y - 0.2f);
                Instantiate(changingDirection, positionOfTheParticles, rotationOfPar);
            }
            spriteRender.flipX = left;
        }
        headingDirection = left;
    }



    // Check if we should instantiate the particle system of the changing direction
    // Requirement:
    // The player is standing on the ground
    // The absolute speed is greater than the specific valor
    private bool IfTheParticleSystemCanInstantiate(bool thePlayerIsGrounded, float speed)
    {
        return thePlayerIsGrounded
            && Mathf.Abs(speed) > 1;
    }



    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Enemies" || otherObject.tag == "DeathZones")
        {
            die();
        }
        if(otherObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint!");
            positionToRespawn = otherObject.transform.position;
        }
    }

   

    private void die()
    {
        gameOver = true;
        
    }


    // Check if the characeter is on the ground
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.04f, groundLayer);
    }


    // Removing all the forces from out Rigidbody 
    // Moving the player to the last checkpoint
    public void restartFromTheLastCheckpoint()
    {
        rb2d.velocity = Vector3.zero;
        transform.position = positionToRespawn;
    }
}
