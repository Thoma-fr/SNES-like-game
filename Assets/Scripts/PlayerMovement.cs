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
    private Vector3 target = Vector3.zero;
    private float step;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !_action)
        {
            _action = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) && _action)
        {
            _rb.AddForce(new Vector2(_force, 0));
            _action = false;
        }
        else if ( (Input.GetKeyDown(KeyCode.LeftArrow) && _action) || Input.GetKeyDown(KeyCode.Keypad6) && !_action)
        {
            _action = false;
        }

        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed, 0);
        }

        _speed = _rb.velocity.x;

        if (target != Vector3.zero)
        {
            step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position,target) <= 1)
            {
                target = Vector3.zero;
            }
        }
    }

    public void GoTo(Vector3 position)
    {
        target = position;
    }
    public void move()
    {
        _rb.AddForce(new Vector2(_force, 0));
    }
}
