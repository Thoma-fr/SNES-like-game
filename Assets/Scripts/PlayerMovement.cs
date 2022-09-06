using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rb;
    private bool _action = true;
    public float _speed = 200;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && _action)
        {
            _rb.AddForce(new Vector2(_speed,0));
            _action = !_action;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !_action)
        {
            _rb.AddForce(new Vector2(_speed,0));
            _action = !_action;
        }
    }
}
