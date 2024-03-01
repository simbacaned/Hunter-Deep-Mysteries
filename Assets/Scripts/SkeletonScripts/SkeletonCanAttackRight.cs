using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkeletonCanAttackRight : MonoBehaviour
{
    //Skeleton animator
    public Animator animator;

    //Boolean to tell the skeleton is a player is still near
    public bool isNearPlayer = false;

    //Boolean that will be passed to skeleton script
    public bool skeletonCanShoot = false;

    //Game object that stores the player
    public GameObject player;

    //Check if a player is in a radius of them
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                isNearPlayer = true;
                player = collision.gameObject;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer && !animator.GetBool("isMovingLeft"))
        {
            double pPosX = player.transform.position.x;
            double sPosX = transform.position.x;
            double distance = pPosX - sPosX;
            if (distance < 5d && distance > 0d)
            {
                skeletonCanShoot = true;
            }
            else
            {
                isNearPlayer = false;
            }
        }
        else
        {
            skeletonCanShoot = false;
        }
    }
}
