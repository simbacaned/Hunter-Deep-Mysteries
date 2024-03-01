using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    //Has wood or water
    public bool hasWood;
    public bool hasWater;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Wood").gameObject.SetActive(false);
        gameObject.transform.Find("Bucket of Water").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hasWater = GameObject.Find("Death Detections").GetComponent<collisionDetection>().hasWater;
        hasWood = GameObject.Find("Death Detections").GetComponent<collisionDetection>().hasWood;
        if (hasWood)
        {
            gameObject.transform.Find("Wood").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("Wood").gameObject.SetActive(false);
        }

        if (hasWater)
        {
            gameObject.transform.Find("Bucket of Water").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("Bucket of Water").gameObject.SetActive(false);
        }
    }
}
