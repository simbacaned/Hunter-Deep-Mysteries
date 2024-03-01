using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiveLevers : MonoBehaviour
{
    //The queue
    public Queue collectiveLevers = new Queue();

    //Arrays for all the different coloured platforms
    GameObject[] bluePlatforms;
    GameObject[] redPlatforms;
    GameObject[] yellowPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        //De-activate blue platforms
        bluePlatforms = GameObject.FindGameObjectsWithTag("Blue Platform");
        for (int i = 0; i < bluePlatforms.Length; i++)
        {
            for (int j = 0; j < bluePlatforms[i].transform.childCount; j++)
            {
                GameObject currentChild = bluePlatforms[i].transform.GetChild(j).gameObject;
                currentChild.SetActive(false);
            }
        }

        //De-activate red platforms
        redPlatforms = GameObject.FindGameObjectsWithTag("Red Platform");
        for (int i = 0; i < redPlatforms.Length; i++)
        {
            for (int j = 0; j < redPlatforms[i].transform.childCount; j++)
            {
                GameObject currentChild = redPlatforms[i].transform.GetChild(j).gameObject;
                currentChild.SetActive(false);
            }
        }

        //De-activate yellow platforms
        yellowPlatforms = GameObject.FindGameObjectsWithTag("Yellow Platform");
        for (int i = 0; i < yellowPlatforms.Length; i++)
        {
            for (int j = 0; j < yellowPlatforms[i].transform.childCount; j++)
            {
                GameObject currentChild = yellowPlatforms[i].transform.GetChild(j).gameObject;
                currentChild.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(collectiveLevers.Count == 3)
        {
            collectiveLevers.Dequeue();
        }

        //Check if the red lever is flipped
        if (collectiveLevers.Contains("Lever_Red"))
        {
            for (int i = 0; i < redPlatforms.Length; i++)
            {
                for (int j = 0; j < redPlatforms[i].transform.childCount; j++)
                {
                    GameObject currentChild = redPlatforms[i].transform.GetChild(j).gameObject;
                    currentChild.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < redPlatforms.Length; i++)
            {
                for (int j = 0; j < redPlatforms[i].transform.childCount; j++)
                {
                    GameObject currentChild = redPlatforms[i].transform.GetChild(j).gameObject;
                    currentChild.SetActive(false);
                }
            }
        }

        //Check if the blue lever is flipped
        if (collectiveLevers.Contains("Lever_Blue"))
        {
            for (int i = 0; i < bluePlatforms.Length; i++)
            {
                for (int j = 0; j < bluePlatforms[i].transform.childCount; j++)
                {
                    GameObject currentChild = bluePlatforms[i].transform.GetChild(j).gameObject;
                    currentChild.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < bluePlatforms.Length; i++)
            {
                for (int j = 0; j < bluePlatforms[i].transform.childCount; j++)
                {
                    GameObject currentChild = bluePlatforms[i].transform.GetChild(j).gameObject;
                    currentChild.SetActive(false);
                }
            }
        }

        //Check if the yellow lever is flipped
        if (collectiveLevers.Contains("Lever_Yellow"))
        {
            for (int i = 0; i < yellowPlatforms.Length; i++)
            {
                for (int j = 0; j < yellowPlatforms[i].transform.childCount; j++)
                {
                    GameObject currentChild = yellowPlatforms[i].transform.GetChild(j).gameObject;
                    currentChild.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < yellowPlatforms.Length; i++)
            {
                for (int j = 0; j < yellowPlatforms[i].transform.childCount; j++)
                {
                    GameObject currentChild = yellowPlatforms[i].transform.GetChild(j).gameObject;
                    currentChild.SetActive(false);
                }
            }
        }
    }

}
