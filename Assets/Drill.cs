using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {Debug.Log("ayay");
        if(collision.CompareTag("Obstacle"))
        {
            
            Destroy(collision.gameObject);
        }
    }
}
