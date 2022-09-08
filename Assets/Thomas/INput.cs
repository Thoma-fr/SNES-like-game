using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("j1 right");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            Debug.Log("j1 trigger L");
        }
        else if(Input.GetKeyDown(KeyCode.Joystick2Button4))
        {
            Debug.Log("j2 trigger L");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button6))
        {
            Debug.Log("j2 trigger R");
        }

    }


}
