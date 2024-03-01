using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHA : MonoBehaviour
{
    //Stun timer
    private float resetCount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Skeleton" || collision.gameObject.tag == "Goblin")
            {
                Destroy(collision.transform.parent.gameObject);
            }
            if(collision.gameObject.tag == "Wolf")
            {
                collision.gameObject.GetComponentInParent<WolfMovement>().isStunned = true;
                collision.gameObject.GetComponentInParent<WolfMovement>().stunTimer = collision.gameObject.GetComponentInParent<WolfMovement>().resetTimer;
            }
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
