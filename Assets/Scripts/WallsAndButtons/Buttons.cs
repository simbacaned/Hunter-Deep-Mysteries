using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Buttons : MonoBehaviour
{
    //String to store the name of the current button
    private string currentButtonName;

    //Boolean to tell the script if the player is still near
    public bool isNearPlayer;

    //Will store all buttons and current button
    Queue allButtons = new Queue();

    //Boolean to check if the current button is already in the queue
    bool isAlreadyInQueue = false;
    GameObject currentButton;

    //Will store the player as a gameObject
    private GameObject playerGameObject;
    private GameObject playerGameObjectHitbox;

    //Content for red button sprite
    private Sprite rButton;

    //Check if the player has collided
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isNearPlayer = true;
            currentButtonName = gameObject.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerGameObjectHitbox = playerGameObject.transform.Find("Death Detections").gameObject;
        rButton = Resources.Load<Sprite>("RedButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer)
        {
            isAlreadyInQueue = false;
            allButtons = GameObject.FindGameObjectWithTag("Walls").GetComponent<Walls>().collectiveButtons;
            double playerX = playerGameObjectHitbox.transform.position.x;
            double playerY = playerGameObjectHitbox.transform.position.y;
            double buttonPosX = gameObject.transform.position.x;
            double buttonPosY = gameObject.transform.position.y+1;
            double distanceX = Math.Sqrt(Math.Pow((playerX - buttonPosX), 2));
            double distanceY = Math.Sqrt(Math.Pow((playerY - buttonPosY), 2));
            double buttonWidth = gameObject.GetComponent<BoxCollider2D>().bounds.size[0];
            double buttonHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size[1];
            if (distanceX < buttonWidth && distanceY < buttonHeight)
            {
                playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (allButtons.Contains(currentButtonName))
                    {
                        isAlreadyInQueue = true;
                    }
                    if (!isAlreadyInQueue)
                    {
                        isNearPlayer = false;
                        playerGameObjectHitbox.transform.Find("Question").gameObject.SetActive(false);
                        GameObject.FindGameObjectWithTag("Walls").GetComponent<Walls>().collectiveButtons.Enqueue(currentButtonName);
                        Destroy(gameObject.GetComponent<BoxCollider2D>());
                        Destroy(gameObject.GetComponent<Buttons>());
                        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                        renderer.sprite = rButton;
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
