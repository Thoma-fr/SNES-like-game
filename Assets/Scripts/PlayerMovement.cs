using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [Header("Info")]
    public Rigidbody2D _rb;
    public float _actual_speed;

    [Header("Set Movement")]
    public float _speed_jump = 20;
    public float _force = 100;
    public float _maxSpeed = 20;
    private bool _action = false;
    private Vector3 target = Vector3.zero;
    private float step;

    [Header("Vignette")]
    public Volume volume;
    public float _vignette_speed = 0.001f;
    public float _vignette_max = 0.5f;
    private Vignette vignette;
    private bool _light = false;

    private void Start()
    {

        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.overrideState = true;
        }
    }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _light = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            _light = false;
        }

        if (_light)
        {
            Debug.Log("LIGHT ON");
            vignette.intensity.value -= _vignette_speed / 1000;
        }else if (vignette.intensity.value < _vignette_max)
        {
            Debug.Log("LIGHT OFF");
            vignette.intensity.value += _vignette_speed / 1000;
            if (vignette.intensity.value > _vignette_max)
            {
                vignette.intensity.value = _vignette_max;
            }
        }

        _actual_speed = _rb.velocity.x;

        if (target != Vector3.zero)
        {
            step = _speed_jump * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position,target) <= 1)
            {
                _rb.velocity = new Vector2(_speed_jump, 0);
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
