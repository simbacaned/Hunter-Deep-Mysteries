using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    //Determines which directions the arrow is travelling
    public float whichDirection = -1f;

    //Lifespan of an arrow
    public float lifespan = 5f;

    //velocity
    public float velocity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= 0.01f;
        if(lifespan < 0)
        {
            Destroy(gameObject);
        }
        transform.position = transform.position + new Vector3(whichDirection * Time.fixedDeltaTime * velocity, 0, 0);
    }
}
