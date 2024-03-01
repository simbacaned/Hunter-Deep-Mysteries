using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class keyBehaviour : MonoBehaviour
{
    //String to store the name of the current button
    private string currentKeyName;

    //Boolean to tell the script if the player is still near
    public bool isNearPlayer;

    //Will store all buttons and current button
    private Queue allKeys = new Queue();

    //Boolean to check if the current button is already in the queue
    GameObject currentKey;

    //Will store the player as a gameObject
    private GameObject playerGameObject;
    private GameObject playerGameObjectHitbox;

    //Check if the player has collided
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isNearPlayer = true;
            currentKeyName = gameObject.name;
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
            allKeys = playerGameObjectHitbox.GetComponent<collisionDetection>().collectiveKeys;
            double playerX = playerGameObjectHitbox.transform.position.x;
            double playerY = playerGameObjectHitbox.transform.position.y;
            double keyPosX = gameObject.transform.position.x;
            double keyPosY = gameObject.transform.position.y + 1;
            double distanceX = Math.Sqrt(Math.Pow((playerX - keyPosX), 2));
            double distanceY = Math.Sqrt(Math.Pow((playerY - keyPosY), 2));
            double keyWidth = gameObject.GetComponent<BoxCollider2D>().bounds.size[0];
            double keyHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size[1];
            if (distanceX < keyWidth && distanceY < keyHeight)
            {
                playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isNearPlayer = false;
                    playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(false);
                    playerGameObjectHitbox.GetComponent<collisionDetection>().collectiveKeys.Enqueue(currentKeyName);
                    Destroy(gameObject);
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

