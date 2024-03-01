using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveLevers : MonoBehaviour
{
    //String to store the name of the current lever
    private string currentLeverName;

    //Boolean to tell the script if the player is still near
    public bool isNearPlayer;

    //Will store the player as a gameObject
    private GameObject playerGameObject;
    private GameObject playerGameObjectHitbox;

    //Will store all levers and current lever
    Queue allLevers = new Queue();
    GameObject currentLever;

    //array which stores only the 2 of an amount of currently active levers
    public Queue<GameObject> currentLevers;

    //Boolean to check if the current lever is already in the queue
    bool isAlreadyInQueue = false;

    //Check if the player has collided
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isNearPlayer = true;
            currentLeverName = gameObject.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerGameObjectHitbox = playerGameObject.transform.Find("Death Detections").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer)
        {
            isAlreadyInQueue = false;
            allLevers = gameObject.GetComponentInParent<CollectiveLevers>().collectiveLevers;
            double playerX = playerGameObjectHitbox.transform.position.x;
            double playerY = playerGameObjectHitbox.transform.position.y;
            double leverPosX = gameObject.transform.position.x;
            double leverPosY = gameObject.transform.position.y;
            double distanceX = Math.Sqrt(Math.Pow((playerX - leverPosX), 2));
            double distanceY = Math.Sqrt(Math.Pow((playerY - leverPosY), 2));
            double leverWidth = gameObject.GetComponent<BoxCollider2D>().bounds.size[0];
            double leverHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size[1];
            if (distanceX < leverWidth && distanceY < leverHeight)
            {
                playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(allLevers.Contains(currentLeverName))
                    {
                        isAlreadyInQueue = true;
                    }
                    if (!isAlreadyInQueue)
                    {
                        gameObject.GetComponentInParent<CollectiveLevers>().collectiveLevers.Enqueue(currentLeverName);
                        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        }
                    }
                }
            }
            else
            {
                isNearPlayer = false;
                playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(false);
            }
        }
    }
}
