using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    public Camera _cam;

    // Update is called once per frame
    void Update()
    {
        _cam.transform.rotation =  Quaternion.Euler(0,0,0);
    }
}
