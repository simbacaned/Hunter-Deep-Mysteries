using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector2 Location;
    public Animator animator;

    //Boolean to determine if you want the platform to move vertically or horizontally
    public bool doesMoveVertically = true;

    //Distance the platform will travel before turning
    public float distance = 800.00f;

    //Counter for distance
    public float distanceTravelled = 400f;

    //Left or right?
    public float whichDirection = -1f;

    //velocity 
    public float velocity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //y position of platform
        float posY = transform.position.y;

        //increment distanceTravelled
        distanceTravelled += 1;

        //Check distance travelled by platform
        if(distanceTravelled > distance)
        {
            distanceTravelled = 0;
            whichDirection = whichDirection * -1;
        }
    }

    void FixedUpdate()
    {
        //Move the platform vertically
        if (doesMoveVertically)
        {
            transform.position = transform.position + new Vector3(0, whichDirection * Time.fixedDeltaTime * velocity, 0);
        }

        //Move the platform horizontally
        if (!doesMoveVertically)
        {
            transform.position = transform.position + new Vector3(whichDirection * Time.fixedDeltaTime * velocity, 0, 0);
        }
    }
}
