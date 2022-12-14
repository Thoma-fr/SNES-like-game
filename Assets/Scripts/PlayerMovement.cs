using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
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
    public float time_light = 0.1f;
    private float _time_lightP1 = 1f;
    private float _time_lightP2 = 1f;
    private float _light_P1 = 0f;
    private float _light_P2 = 0f;
    private float _lightdown_P1 = 0f;
    private float _lightdown_P2 = 0f;
    private bool _lightP1 = false;
    private bool _lightP2 = false;

    [Header("Gravity")]
    public float floatHeight = 10;
    public float gravity = 10;

    private PlayerLucasG playerInfo;
    public Sprite[] simple;
    public Sprite[] damaged;

    private SpriteRenderer sprite;

    private bool canMove;
    private void Start()
    {
        playerInfo=GetComponent<PlayerLucasG>();
        sprite=GetComponent<SpriteRenderer>();
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.active = true;
        }

        var hold_light_p1_action = new InputAction("fire");
        hold_light_p1_action.AddBinding("<HID::USB gamepad           >/button4")
            .WithInteractions("hold");

        hold_light_p1_action.performed +=
            context =>
            {
                if (context.interaction is HoldInteraction)
                    _lightP1 = true;
            };

        hold_light_p1_action.canceled +=
            _ => _lightP1 = false;


        hold_light_p1_action.Enable();

        var hold_light_p2_action = new InputAction("fire");
        hold_light_p2_action.AddBinding("<HID::USB Gamepad >/button4")
            .WithInteractions("hold");

        hold_light_p2_action.performed +=
            context =>
            {
                if (context.interaction is HoldInteraction)
                    _lightP2 = true;
            };

        hold_light_p2_action.canceled +=
            _ => _lightP2 = false;


        hold_light_p2_action.Enable();

    }

    private void FixedUpdate()
    {
        Vector3 localEuler = transform.localEulerAngles;
        if (localEuler.z > 180f)
        {
            localEuler.z -= 360;
        }
        localEuler.z = Mathf.Clamp(localEuler.z, -30, 30);
        transform.localEulerAngles = localEuler;
    }

    void Update()
    {

        if (_lightP1)
        {
            hold_light_p1();
        }

        if (_lightP2)
        {
            hold_light_p2();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !_action)
        {
            _action = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) && _action)
        {
            if (canMove)
            {
                _rb.AddForce(new Vector2(_force, 0));
                _action = false;
            }
        }

        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed, 0);
        }

        _time_lightP1 -= Time.deltaTime;
        if ( _time_lightP1 <= 0 )
        {
            _light_P1 = 0;

            if (_lightdown_P1 < _vignette_max)
            {
                vignette.intensity.value += _vignette_speed / 1000;
                _lightdown_P1 += _vignette_speed / 1000;
                _light_P1 -= _vignette_speed / 1000;

                if (vignette.intensity.value > _vignette_max * 2)
                {
                    vignette.intensity.value = _vignette_max * 2;
                }
            }
        }

        _time_lightP2 -= Time.deltaTime;
        if (_time_lightP2 <= 0 && _light_P2 > 0)
        {
            if (_lightdown_P2 < _vignette_max)
            {
                vignette.intensity.value += _vignette_speed / 1000;
                _lightdown_P2 += _vignette_speed / 1000;
                _light_P2 -= _vignette_speed / 1000;

                if (vignette.intensity.value > _vignette_max * 2)
                {
                    vignette.intensity.value = _vignette_max * 2;
                }
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

        int layerMask = 1 << LayerMask.NameToLayer("Collider");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,floatHeight, layerMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * floatHeight, Color.red);

        Debug.Log(hit.point);
        Debug.Log(transform.position.y- hit.point.y );
        if(transform.position.y - hit.point.y >5)
        {
            _rb.gravityScale = 20;
        }
        else if (transform.position.y - hit.point.y <2)
        {
            _rb.gravityScale = 1;
        }
            if (hit)
        if (hit.transform.CompareTag("ground"))
        {
            
            canMove = true;
            //_rb.gravityScale = 1;
        }
        else if (hit.collider==null)
        {

            Debug.Log("touche pas le sol");
            _rb.AddForce(new Vector2(0, -gravity));
            canMove = false;
            _action = false;
            //_rb.gravityScale = 2000;
        }
        //transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(transform.localRotation.eulerAngles.z, -30, 30));


    }


    public void GoTo(Vector3 position)
    {
        target = position;
    }

    public void hold_light_p1()
    {
        if (_light_P1 < _vignette_max)
        {
            vignette.intensity.value -= _vignette_speed / 1000;
            _light_P1 += _vignette_speed / 1000;
            _lightdown_P1 -= _vignette_speed / 1000;
        }
        _time_lightP1 = time_light;
    }

    public void hold_light_p2()
    {
        if (_light_P2 < _vignette_max)
        {
            vignette.intensity.value -= _vignette_speed / 1000;
            _light_P2 += _vignette_speed / 1000;
            _lightdown_P2 -= _vignette_speed / 1000;
        }
        _time_lightP2 = time_light;
    }

    public void move_p1()
    {
        _action = true;
        StartCoroutine(P1());
    }

    public void move_p2()
    {
        if (_action)
        {
            _rb.AddForce(new Vector2(_force, 0));
            _action = false;
            StartCoroutine(P2());
        }
    }
    public void changeSpriteplayer1up()
    {
        StartCoroutine(P1());
    }
    IEnumerator P1()
    {
        
        Debug.Log(simple[2]);
        if(playerInfo.hp>2)
        {
            Debug.Log("courou");
            sprite.sprite = simple[0];
            yield return new WaitForSeconds(0.2f);
            sprite.sprite = simple[1];
        }
        else
        {
            sprite.sprite = damaged[0];
            yield return new WaitForSeconds(0.2f);
            sprite.sprite = damaged[1];
        }

        
    }
    IEnumerator P2()
    {
        
        Debug.Log(simple[2]);
        if (playerInfo.hp > 2)
        {
            Debug.Log("courou");
            sprite.sprite = simple[2];
            yield return new WaitForSeconds(0.2f);
            sprite.sprite = simple[3];
        }
        else
        {
            sprite.sprite = damaged[2];
            yield return new WaitForSeconds(0.2f);
            sprite.sprite = damaged[3];
        }
        
    }

}
