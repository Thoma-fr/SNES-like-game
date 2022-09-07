using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickaxe : MonoBehaviour
{
    private Animator anim;
    private Collider2D collider2d;

    private void Start()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("hit an obstacle");
            Destroy(collision.gameObject);
        }
    }
}
