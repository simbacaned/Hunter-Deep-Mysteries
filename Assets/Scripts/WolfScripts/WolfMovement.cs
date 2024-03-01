using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    //Boolean to check if the wolf is stunned
    public bool isStunned = false;
    public float stunTimer;
    public float resetTimer = 1f;

    //Load position and animator
    public Vector2 Location;
    public Animator animator;

    //Distance the Wolf will travel before turning
    public float distance = 800.00f;

    //Counter for distance
    public float distanceTravelled = 400f;

    //Left or right?
    public float leftOrRight = -1f;

    //Velocity
    public float velocity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //x and y position of Wolf
        float posY = transform.position.y;
        float posX = transform.position.x;

        //increment distanceTravelled
        if (!isStunned)
        {
            distanceTravelled += 1;
        }

        //Check distance travelled by Wolf
        if (distanceTravelled > distance)
        {
            distanceTravelled = 0;
            leftOrRight = leftOrRight * -1;
        }

        //Update the animator
        if (leftOrRight == -1f)
        {
            animator.SetBool("isMovingLeft", true);
        }
        else
        {
            animator.SetBool("isMovingLeft", false);
        }
        if (isStunned)
        {
            animator.SetBool("IsStunned", true);
            stunTimer -= 0.01f;
            gameObject.transform.Find("Hitbox").gameObject.SetActive(false);
            if (stunTimer < 0f)
            {
                isStunned = false;
                animator.SetBool("IsStunned", false);
            }
        }
    }

    //Update movement of Wolf
    void FixedUpdate()
    {
        if (!isStunned)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            animator.SetBool("IsStunned", false);
            transform.position = transform.position + new Vector3(leftOrRight * Time.fixedDeltaTime * velocity, 0, 0);
        }
    }
}
