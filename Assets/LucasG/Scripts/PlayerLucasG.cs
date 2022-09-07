using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLucasG : MonoBehaviour
{
    public static PlayerLucasG Instance { get; private set; }

    public bool canDestroy = false;
    private GameObject objectToDestroy;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canDestroy)
            {
                Destroy(objectToDestroy);
            }
        }
    }

    public void AssignObstacleToDestroy(GameObject gameObj)
    {
        Debug.Log("assigned object to destroy");
        objectToDestroy = gameObj;
    }
}
