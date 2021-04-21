using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player character
public class Player_Controller : Attack_Object
{
    private Rigidbody _mainRB;
    private Animator _animator;

    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;

    [SerializeField]
    private float _runSpeed = 1;

    new void Start()
    {
        base.Start();
        _mainRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    new void Update()
    {
        base.Update();
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");   
           
        // Character is running
        if (_horizontal != 0 || _vertical != 0)
        {
            // Rotate character towards movement                 public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta);
            float singleStep = 10f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(_horizontal, 0.0f, _vertical), singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            _animator.SetInteger("_animState", 1);
        }
        // Character standing still
        else
        {
            _animator.SetInteger("_animState", 0);
        }
    }

    private void FixedUpdate()
    {
        if (_horizontal != 0 && _vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, otherwise the player would move faster diagonally
            _horizontal *= _moveLimiter;
            _vertical *= _moveLimiter;
        }

        _mainRB.velocity = new Vector3(_horizontal * _runSpeed, 0,  _vertical * _runSpeed);
    }

}
