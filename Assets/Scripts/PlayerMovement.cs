using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float _speed;
    public float _force = 100;
    public float _maxSpeed = 20;
    private bool _action = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !_action)
        {
            _action = true;
            Debug.Log("LEFT");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _action)
        {
            _rb.AddForce(new Vector2(_force, 0));
            _action = false;
            Debug.Log("RIGHT");
        }
        else if ( (Input.GetKeyDown(KeyCode.LeftArrow) && _action) || Input.GetKeyDown(KeyCode.RightArrow) && !_action)
        {
            _action = false;
            Debug.Log("WRONG");
        }

        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed, 0);
        }

        _speed = _rb.velocity.x;
    }
}
