using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLaneTrigger : MonoBehaviour
{
    [SerializeField]
    private bool _p1_up = false;
    [SerializeField]
    private bool _p1_down = false;
    [SerializeField]
    private bool _p2_up = false;
    [SerializeField]
    private bool _p2_down = false;
    [SerializeField]
    private bool _changeLane = false;

    [SerializeField]
    private GameObject _player_go = null;
    public PlayerMovement _player_movement = null;
    private Rigidbody2D _player_rb = null;

    public GameObject point_up;
    public GameObject point_down;

    private void Start()
    {
        _player_movement = _player_go.GetComponent<PlayerMovement>();
        _player_rb = _player_go.GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _changeLane = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _changeLane)
        {
            if (_p1_up == _p2_up && _p1_up)
            {
                _player_movement.GoTo(point_up.transform.position);
            }
            else if (_p1_down == _p2_down && _p1_down)
            {
                _player_movement.GoTo(point_down.transform.position);
            }
            else
            {
                _player_rb.velocity = new Vector2(5, 0);
            }
            _changeLane = false;
        }
    }

    public void UP_P1()
    {
        _p1_up = true;
        _p1_down = false;
    }
    public void DOWN_P1()
    {
        _p1_up = false;
        _p1_down = true;
    }

    public void UP_P2()
    {
        _p2_up = true;
        _p2_down = false;
    }

    public void DOWN_P2()
    {
        _p2_up = false;
        _p2_down = true;
    }
}
