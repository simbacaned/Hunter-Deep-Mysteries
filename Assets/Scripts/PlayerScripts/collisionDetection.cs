using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class collisionDetection : MonoBehaviour
{
    //Used to store the game object Gas Platforms
    public GameObject gasPlatform;

    //Used to determine which keys you have
    public Queue collectiveKeys = new Queue();

    //Used to check if the player is near a tutorial note
    public bool nearTutorialNote = false;

    //Used to store the game objects tutorial note
    public GameObject[] tutorialNotes;
    public GameObject currentTutorialNote;

    private Rigidbody2D myRigidBody;

    //Initial spawn location
    public Vector2 respawnLocation = new Vector2(-24f, -10.5f);

    //Timer for gas platforms
    public float gasTimer = 0f;
    private bool isTiming = false;

    //Player resources
    public bool hasWater = false;
    public bool hasWood = false;
    public bool isNearFire = false;
    public bool isNearBridge = false;
    public bool isNearWater = false;
    public bool isNearWood = false;

    //Temporary storage
    private GameObject currentWater;
    private GameObject currentWood;
    private GameObject currentBridge;
    private GameObject currentFire;

    //Find bridges
    private GameObject[] bridges;

    //Checkpoints
    private Sprite checkpointActive;
    private Sprite checkpointInactive;

    //Box collider
    private BoxCollider2D colliders;

    //Store all checkpoints
    private GameObject[] checkPoints;

    //Collision with enemy detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            //Collision with damaging objects
            if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Goblin" || collision.gameObject.tag == "Wolf")
            {
                transform.parent.position = respawnLocation;
                gameObject.transform.position = respawnLocation;
            }

            //Collision with a checkpoint
            if(collision.gameObject.tag == "Checkpoint")
            {
                checkPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
                for (int i = 0; i < checkPoints.Length; i++)
                {
                    SpriteRenderer cRenderer = checkPoints[i].GetComponent<SpriteRenderer>();
                    cRenderer.sprite = checkpointInactive;
                }
                float posY = collision.transform.position.y;
                float posX = collision.transform.position.x;
                respawnLocation = new Vector2(posX, posY + 0.1f);
                SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
                renderer.sprite = checkpointActive;
            }

            //Collision with gas particles
            if (collision.gameObject.tag == "Gas")
            {
                gasPlatform = GameObject.FindGameObjectWithTag("Gas Platforms");
                for (int i = 0; i < gasPlatform.transform.childCount; i++)
                {
                    GameObject currentChild = gasPlatform.transform.GetChild(i).gameObject;
                    currentChild.SetActive(true);
                }
                gasTimer = 10f;
                isTiming = true;
            }

            //Near a tutorial note
            if (collision.gameObject.tag == "Tutorial Note")
            {
                currentTutorialNote = collision.gameObject.transform.Find("Help 1").gameObject;
                nearTutorialNote = true;
            }

            //Near a bridge
            if (collision.gameObject.tag == "Bridge")
            {
                currentBridge = collision.gameObject;
                isNearBridge = true;
            }

            //Near a fire
            if (collision.gameObject.tag == "Fire")
            {
                currentFire = collision.gameObject;
                isNearFire = true;
            }

            //Near water
            if (collision.gameObject.tag == "Water")
            {
                currentWater = collision.gameObject;
                isNearWater = true;
            }

            //Near wood
            if (collision.gameObject.tag == "Wood")
            {
                currentWood = collision.gameObject;
                isNearWood = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        //De-activate exclamation mark
        gameObject.transform.Find("Exclamation").gameObject.SetActive(false);

        //De-activate question mark
        gameObject.transform.Find("Question").gameObject.SetActive(false);

        //De-activate bridges
        bridges = GameObject.FindGameObjectsWithTag("Bridge");
        for (int i = 0; i < bridges.Length; i++)
        {
            for (int j = 2; j < bridges[i].transform.childCount; j++)
            {
                GameObject currentChild = bridges[i].transform.GetChild(j).gameObject;
                currentChild.SetActive(false);
            }
        }

        //Make all notes inactive
        tutorialNotes = GameObject.FindGameObjectsWithTag("Tutorial Note");
        for (int i = 0; i < tutorialNotes.Length; i++)
        {
            GameObject currentChild = tutorialNotes[i].transform.GetChild(0).gameObject;
            currentChild.SetActive(false);
        }

        //Make all gas platforms inactive
        gasPlatform = GameObject.FindGameObjectWithTag("Gas Platforms");
        for (int i = 0; i < gasPlatform.transform.childCount; i++)
        {
            GameObject currentChild = gasPlatform.transform.GetChild(i).gameObject;
            currentChild.SetActive(false);
        }

        //Load active checkpoint resources
        checkpointActive = Resources.Load<Sprite>("Checkpoint Blue");
        checkpointInactive = Resources.Load<Sprite>("Checkpoint");

        //Box collider attribute
        colliders = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player's position
        double pPosX = transform.position.x;
        double pPosY = transform.position.y;

        if (gasTimer < 0f)
        {
            gasPlatform = GameObject.FindGameObjectWithTag("Gas Platforms");
            for (int i = 0; i < gasPlatform.transform.childCount; i++)
            {
                GameObject currentChild = gasPlatform.transform.GetChild(i).gameObject;
                currentChild.SetActive(false);
            }
            isTiming = false;
        }
        if (isTiming)
        {
            gasTimer -= 0.01f;
        }

        //Checking if the player is still near a bridge
        if (isNearBridge)
        {
            double childX = currentBridge.transform.position.x;
            double distance = Math.Sqrt(Math.Pow((childX - pPosX), 2));
            if (distance < 5d)
            {
                gameObject.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && hasWood)
                {
                    for (int j = 2; j < currentBridge.transform.childCount; j++)
                    {
                        gameObject.transform.Find("Question").gameObject.SetActive(false);
                        GameObject currentChild = currentBridge.transform.GetChild(j).gameObject;
                        currentChild.SetActive(true);
                    }
                    hasWood = false;
                    isNearBridge = false;
                    Destroy(currentBridge.GetComponent<BoxCollider2D>());
                }
            }
            else
            {
                isNearBridge = false;
                gameObject.transform.Find("Question").gameObject.SetActive(false);
            }
        }

        //Checking if the player is still near a firewall
        if (isNearFire)
        {
            double childX = currentFire.transform.position.x+2d;
            double distance = Math.Sqrt(Math.Pow((childX - pPosX), 2));
            if (distance < 2d)
            {
                gameObject.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && hasWater)
                {
                    for (int i = 0; i < currentFire.transform.childCount; i++)
                    {
                        gameObject.transform.Find("Question").gameObject.SetActive(false);
                        GameObject currentChild = currentFire.transform.GetChild(i).gameObject;
                        currentChild.SetActive(false);
                    }
                    hasWater = false;
                    isNearFire = false;
                    Destroy(currentFire.GetComponent<BoxCollider2D>());
                }
            }
            else
            {
                isNearFire = false;
                gameObject.transform.Find("Question").gameObject.SetActive(false);
            }
        }

        //Checking if the player is still near water
        if (isNearWater)
        {
            double childX = currentWater.transform.position.x;
            double childY = currentWater.transform.position.y + 0.75;
            double distanceX = Math.Sqrt(Math.Pow((childX - pPosX), 2));
            double distanceY = Math.Sqrt(Math.Pow((childY - pPosY), 2));
            double waterWidth = currentWater.GetComponent<BoxCollider2D>().bounds.size[0];
            double waterHeight = currentWater.GetComponent<BoxCollider2D>().bounds.size[1];
            if (distanceX <= waterWidth && distanceY <= waterHeight)
            {
                gameObject.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !hasWater)
                {
                    hasWater = true;
                    Destroy(currentWater);
                    isNearWater = false;
                    gameObject.transform.Find("Question").gameObject.SetActive(false);
                }
            }
            else
            {
                isNearWater = false;
                gameObject.transform.Find("Question").gameObject.SetActive(false);
            }
        }

        //Checking if the player is still near wood
        if (isNearWood)
        {
            double childX = currentWood.transform.position.x;
            double childY = currentWood.transform.position.y + 0.5;
            double distanceX = Math.Sqrt(Math.Pow((childX - pPosX), 2));
            double distanceY = Math.Sqrt(Math.Pow((childY - pPosY), 2));
            double woodWidth = currentWood.GetComponent<BoxCollider2D>().bounds.size[0];
            double woodHeight = currentWood.GetComponent<BoxCollider2D>().bounds.size[1];
            if (distanceX < woodWidth && distanceY < woodHeight)
            {
                gameObject.transform.Find("Question").gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !hasWood)
                {
                    hasWood = true;
                    Destroy(currentWood);
                    isNearWood = false;
                    gameObject.transform.Find("Question").gameObject.SetActive(false);
                }
            }
            else
            {
                isNearWood = false;
                gameObject.transform.Find("Question").gameObject.SetActive(false);
            }
        }

        //Checking if the player is still near a tutorial note
        if (nearTutorialNote)
        {
            double childX = currentTutorialNote.transform.position.x;
            double distanceX = Math.Sqrt(Math.Pow((childX - pPosX), 2));
            if (distanceX < 1.2d) {
                if (!gameObject.transform.Find("Question").gameObject.activeSelf)
                {
                    gameObject.transform.Find("Exclamation").gameObject.SetActive(true);
                }
                else
                {
                    gameObject.transform.Find("Exclamation").gameObject.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if(!currentTutorialNote.activeSelf)
                    currentTutorialNote.SetActive(true);
                    else
                    {
                        currentTutorialNote.SetActive(false);
                    }
                }
            }
            else
            {
                nearTutorialNote = false;
                currentTutorialNote.SetActive(false);
                gameObject.transform.Find("Exclamation").gameObject.SetActive(false);
            }
        }

        //Checking for crouching
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            colliders.size = new Vector2(0.75f, 0.6f);
            colliders.transform.position = new Vector2(colliders.transform.position.x, colliders.transform.position.y - 0.65f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            colliders.size = new Vector2(0.75f, 1.9f);
            colliders.transform.position = new Vector2(colliders.transform.position.x, colliders.transform.position.y + 0.65f);
        }
    }
}
