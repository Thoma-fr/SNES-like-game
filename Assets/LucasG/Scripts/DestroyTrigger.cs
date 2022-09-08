using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DestroyTriggerEntered");
            PlayerLucasG.Instance.AssignObstacleToDestroy(transform.parent.gameObject);
            PlayerLucasG.Instance.canDestroy = true;
            PlayerLucasG.Instance.collisionWarning.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DestroyTriggerLeft");
            PlayerLucasG.Instance.canDestroy = false;
            PlayerLucasG.Instance.collisionWarning.SetActive(false);
        }
    }
}
