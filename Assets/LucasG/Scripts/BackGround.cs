using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        transform.position = player.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.4f*Time.deltaTime) ;
    }
}
