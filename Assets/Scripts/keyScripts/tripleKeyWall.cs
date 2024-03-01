using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tripleKeyWall : MonoBehaviour
{
    //Will store the player as a gameObject
    private GameObject playerGameObject;
    private GameObject playerGameObjectHitbox;
    private Queue theQueue = new Queue();

    //String to store the name of the current button
    private string currentWallName;

    //Boolean to tell the script if the player is still near
    public bool isNearPlayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isNearPlayer = true;
            currentWallName = gameObject.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerGameObjectHitbox = playerGameObject.transform.Find("Death Detections").gameObject;
        theQueue = playerGameObjectHitbox.GetComponent<collisionDetection>().collectiveKeys;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject currentChild = gameObject.transform.GetChild(i).gameObject;
            currentChild.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer)
        {
            double playerX = playerGameObjectHitbox.transform.position.x;
            double playerY = playerGameObjectHitbox.transform.position.y;
            double wallPosX = gameObject.transform.position.x;
            double wallPosY = gameObject.transform.position.y + 1;
            double distanceX = Math.Sqrt(Math.Pow((playerX - wallPosX), 2));
            double distanceY = Math.Sqrt(Math.Pow((playerY - wallPosY), 2));
            double wallWidth = gameObject.GetComponent<BoxCollider2D>().bounds.size[0];
            double wallHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size[1];
            if (distanceX < wallWidth-1 && distanceY < wallHeight)
            {
                playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {
                        if (theQueue.Contains("Key Red") && theQueue.Contains("Key Blue") && theQueue.Contains("Key Green"))
                        {
                            gameObject.transform.GetChild(i).gameObject.SetActive(false);
                            isNearPlayer = false;
                            Destroy(gameObject.GetComponent<BoxCollider2D>());
                            Destroy(gameObject.GetComponent<tripleKeyWall>());
                            playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(false);
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
