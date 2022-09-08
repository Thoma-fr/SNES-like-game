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
    public float _vignette_max = 0.25f;
    private Vignette vignette;
    public bool _light = false;
    public int _nb_light = 0;

    private void Start()
    {

        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.active = true;
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

        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddLight();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            AddLight();
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            RemoveLight();
        }
        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            RemoveLight();
        }

        if (_nb_light == 0)
        {
            _light = false;
        }

        if (_light)
        {
            if (vignette.intensity.value > (_vignette_max * 2 - _vignette_max * _nb_light))
            {
                vignette.intensity.value -= _vignette_speed / 1000;
            }

            if (vignette.intensity.value < (_vignette_max * 2 - _vignette_max * _nb_light))
            {
                vignette.intensity.value += _vignette_speed / 1000;
            }
        }
        else if (vignette.intensity.value < _vignette_max * 2)
        {
            vignette.intensity.value += _vignette_speed / 1000;

            if (vignette.intensity.value > _vignette_max * 2 )
            {
                vignette.intensity.value = _vignette_max * 2;
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

    public void AddLight()
    {
        _light = true;
        _nb_light++;
    }

    public void RemoveLight()
    {
        _nb_light--;
    }

    public void GoTo(Vector3 position)
    {
        target = position;
    }

    public void hold_light_p1()
    {
        AddLight();
        RemoveLight();
    }

    public void hold_light_p2()
    {
        AddLight();
        RemoveLight();
    }

    public void move_p1()
    {
        _action = true;
    }

    public void move_p2()
    {
        if (_action)
        {
            _rb.AddForce(new Vector2(_force, 0));
            _action = false;
        }
    }
}
