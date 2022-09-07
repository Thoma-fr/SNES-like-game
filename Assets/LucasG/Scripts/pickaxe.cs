using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickaxe : MonoBehaviour
{
    private Animator anim;
    private Collider2D collider2d;
    private GameObject parent;
    private Vector2 offSet = new Vector2(1,0.3f);
    
    private void Start()
    {
        parent = transform.parent.gameObject;
        anim = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("colliderIsActive", true);
            collider2d.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("colliderIsActive", false);
            collider2d.enabled = false;
        }

        transform.position = parent.transform.position + (Vector3)offSet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("pickaxe hit an obstacle");
            Destroy(collision.gameObject);
        }
    }
}
