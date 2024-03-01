using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordQA : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Goblin")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
