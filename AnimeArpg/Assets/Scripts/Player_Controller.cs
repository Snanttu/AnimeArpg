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

    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;

    private float _actionSpeed = 1;
    private float _actionActivation;
    private float _actionCooldown;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");        

        if (_actionCooldown <= 0)
        {
            // Character is running
            if (Input.GetButton("Fire1"))
            {
                Skill();
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

            if (_actionCooldown < _actionActivation + 0.05f || _actionCooldown < _actionActivation - 0.05f)
            {
                _hitboxes[0].enabled = true;
            }
            else
            {
                _hitboxes[0].enabled = false;
            }
        }
        else
        {
            _hitboxes[0].enabled = false;
            _animator.speed = 1;
        }
    }

    private void Skill()
    {
        _actionCooldown = 1 / (_attackSpeed * _actionSpeed);
        _actionActivation = _actionCooldown * 0.45f;
        _animator.speed = _attackSpeed * _actionSpeed;
        _animator.SetInteger("_animState", 2);
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

            _mainRB.velocity = new Vector3(_horizontal * (_runSpeed * _actionSpeed), 0, _vertical * (_runSpeed * _actionSpeed));
        }
    }

}
