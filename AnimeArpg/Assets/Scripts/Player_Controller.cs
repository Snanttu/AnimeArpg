using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player character
public class Player_Controller : Attack_Object
{
    [SerializeField]
    private float _runSpeed = 1;
    [SerializeField]
    private Collider[] _hitboxes;

    private Rigidbody _mainRB;
    private Animator _animator;

    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;

    private float _actionSpeed = 1;
    private float _actionActivation;
    private float _actionCooldown;

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

        if (_actionCooldown <= 0)
        {
            // Character is running
            if (Input.GetButtonDown("Fire1"))
            {
                Skill();
                _actionCooldown = 1 / _actionSpeed;
            }
            else if (_horizontal != 0 || _vertical != 0)
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
        
        if (_actionCooldown > 0)
        {
            _actionCooldown -= Time.deltaTime;

            if (_actionCooldown <= _actionActivation)
            {
                _hitboxes[0].enabled = true;
            }
        }
        else
        {
            _hitboxes[0].enabled = false;
        }        
    }

    private void Skill()
    {
        _animator.SetInteger("_animState", 2);
        _actionActivation = 0.5f;
    }

    private void FixedUpdate()
    {
        // Cannot move during an action
        if (_actionCooldown <= 0)
        {
            if (_horizontal != 0 && _vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, otherwise the player would move faster diagonally
                _horizontal *= _moveLimiter;
                _vertical *= _moveLimiter;
            }

            _mainRB.velocity = new Vector3(_horizontal * _runSpeed, 0, _vertical * _runSpeed);
        }
    }

}
