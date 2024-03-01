using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    //The queue
    public Queue collectiveButtons = new Queue();

    //Array to store all walls
    public GameObject[] allWalls;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject currentChild = gameObject.transform.GetChild(i).gameObject;
            currentChild.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (collectiveButtons.Contains(gameObject.transform.GetChild(i).gameObject.name))
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
