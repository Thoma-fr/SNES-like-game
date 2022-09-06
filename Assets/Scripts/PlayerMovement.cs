using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rb;
    private bool _action = true;
    public float _speed;
    public float _force = 100;
    public float _maxSpeed = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && _action)
        {
            _rb.AddForce(new Vector2(_force, 0));
            _action = !_action;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !_action)
        {
            _rb.AddForce(new Vector2(_force, 0));
            _action = !_action;
        }

        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed, 0);
        }

        _speed = _rb.velocity.x;
    }
}
