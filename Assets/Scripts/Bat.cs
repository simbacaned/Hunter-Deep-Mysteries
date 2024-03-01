using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

    public Vector2 Location;
    public Animator animator;

    //Distance the bat will travel before turning
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
        //x and y position of bat
        float posY = transform.position.y;
        float posX = transform.position.x;

        //increment distanceTravelled
        distanceTravelled += 1;

        //Check distance travelled by bat
        if(distanceTravelled > distance)
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
    }
    void FixedUpdate()
    {
        //Move the bat
        transform.position = transform.position + new Vector3(leftOrRight * Time.fixedDeltaTime * velocity, 0, 0);
    }
}
