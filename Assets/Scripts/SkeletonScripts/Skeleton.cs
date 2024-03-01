using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Vector2 Location;
    public Animator animator;

    //Float which determines the value which timerForShooting will return to
    public float returnTime = 5f;

    //Boolean to check if the skeleton is shooting
    bool skeletonIsShooting = false;

    //Boolean to let the skeleton be stationary
    public bool skeletonIsStationary;

    //Velocity
    public float velocity = 1f;

    //Distance the skeleton will travel before turning
    public float distance = 1000.00f;

    //Counter for distance
    public float distanceTravelled = 500f;

    //Left or right?
    public float leftOrRight = -1f;

    //Timer which determines when the skeletons shooting cooldown ends
    public float timerForShooting = 0.0f;

    //Boolean for if the player is near
    public bool playerIsNear = false;

    //Content for arrow sprite
    private Sprite arrowRight;
    private Sprite arrowLeft;

    // Start is called before the first frame update
    void Start()
    {
        arrowRight = Resources.Load<Sprite>("ArrowRight");
        arrowLeft = Resources.Load<Sprite>("ArrowLeft");
    }

    // Update is called once per frame
    void Update()
    {
        //x and y position of skeleton
        float posY = transform.position.y;
        float posX = transform.position.x;

        //increment distanceTravelled
        if (!skeletonIsShooting && !skeletonIsStationary)
        {
            distanceTravelled += 1;
        }

        //Check distance travelled by skeleton
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
        if (skeletonIsStationary)
        {
            animator.SetBool("skeletonIsStationary", true);
        }

        //Update the skeleton shooting timer
        if (GameObject.Find("Skeleton Range Right").GetComponent<SkeletonCanAttackRight>().skeletonCanShoot || GameObject.Find("Skeleton Range Left").GetComponent<SkeletonCanAttackLeft>().skeletonCanShoot) 
        {
            playerIsNear = true;
        }
        else
        {
            playerIsNear = false;
        }
        if (timerForShooting <= 0f)
        {
            if (playerIsNear)
            {
                //Skeleton is shooting
                skeletonIsShooting = true;
                animator.SetBool("skeletonIsShooting", true);

                timerForShooting = returnTime;

                //Creating the arrow
                GameObject arrow = new GameObject("New Sprite");
                arrow.transform.position = gameObject.transform.position;
                arrow.transform.position = new Vector2(arrow.transform.position.x, arrow.transform.position.y + 0.35f);
                SpriteRenderer renderer = arrow.AddComponent<SpriteRenderer>();
                BoxCollider2D collider = arrow.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;
                collider.size = new Vector2(0.6f, 0.2f);
                arrow.AddComponent<arrowScript>();
                arrow.transform.tag = "Spike";
                if (!animator.GetBool("isMovingLeft"))
                {
                    arrow.GetComponent<arrowScript>().whichDirection = 1f;
                    renderer.sprite = arrowRight;
                }
                else
                {
                    arrow.GetComponent<arrowScript>().whichDirection = -1f;
                    renderer.sprite = arrowLeft;
                }
            }
        }
        else
        {
            timerForShooting -= 0.01f;
        }
        if (timerForShooting < returnTime - 0.5f)
        {
            skeletonIsShooting = false;
            animator.SetBool("skeletonIsShooting", false);
        }
    }
    void FixedUpdate()
    {
        //Move the skeleton
        if (!skeletonIsShooting && !skeletonIsStationary)
        {
            transform.position = transform.position + new Vector3(leftOrRight * Time.fixedDeltaTime * velocity, 0, 0);
        }
    }
}
