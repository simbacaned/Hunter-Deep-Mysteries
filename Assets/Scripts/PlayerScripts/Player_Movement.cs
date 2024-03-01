using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Vector2 Location;
    public Animator animator;
    float velY = 0.0f;
    float velX = 0.0f;
    public bool playerCanMove = true;
    float fallingFrames = 0.0f;
    const float gravity = 0.981f; 

    private Rigidbody2D myRigidBody;
    public bool grounded;
    public LayerMask isGround;
    private BoxCollider2D myCollider;

    // Runs once when the program stars
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checking if the player is grounded
        grounded = Physics2D.IsTouchingLayers(myCollider, isGround);

        //Resetting variables and parameters
        animator.SetBool("isMoving", false);
        animator.SetFloat("velX", 0);
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        velX = 0.0f;
        float posY = transform.position.y;
        float posX = transform.position.x;

        //Sideways movement
        float horizontal = Input.GetAxis("Horizontal");
        velX = horizontal;

        //Update animator
        if (grounded)
        {
            fallingFrames = 0;
            if (velX != 0)
            {
                animator.SetFloat("velX", velX);
                animator.SetBool("isMoving", true);
                if (velX < 0)
                {
                    animator.SetBool("facingLeft", true);
                }
                if (velX > 0)
                {
                    animator.SetBool("facingLeft", false);
                }
            }
        }
        if (velY < 0)
        {
            animator.SetBool("jump", false);
            if (grounded)
            {
                velY = 0;
            }
        }

        //Checking key presses
        if (grounded && playerCanMove)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetBool("attack1", true);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.SetBool("attack2", true);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("jump", true);
                velY = 12f;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouching", true);
            playerCanMove = false;
            grounded = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouching", false);
            playerCanMove = true;
        }

        //Gravity
        if (!grounded && velY>-10)
        {
            fallingFrames += 0.1f;
            velY -= (gravity / 10 * fallingFrames);
        }
    }
    void FixedUpdate()
    {
        //Move the player
        if (playerCanMove)
        {
            transform.position = transform.position + new Vector3(velX * Time.fixedDeltaTime * 6, 0, 0);
        }
        transform.position = transform.position + new Vector3(0, velY * Time.deltaTime, 0);
    }
}
