using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player character
public class Player_Controller : Attack_Object
{
    [SerializeField]
    private float _runSpeed = 1;
    [SerializeField]
    private float _jumpForce = 2.0f;
    [SerializeField]
    private LayerMask _whatIsGround;

    private bool _grounded = true;
    private float _horizontal;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Ground check
        Collider[] _groundCheck = Physics.OverlapBox(gameObject.transform.position, new Vector3(1, 0.2f, 0.2f), Quaternion.identity, _whatIsGround);

        if (_groundCheck.Length > 0)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }


        if (_actionCooldown <= 0)
        {
            if (_horizontal != 0)
            {
                // Turn around
                if (_horizontal < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            {
                Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
                _mainRB.AddForce(jump * _jumpForce, ForceMode.Impulse);
                _grounded = false;
            }

            // On the ground
            if (_grounded)
            {
                // Character is performing a skill
                if (Input.GetButtonDown("Fire1"))
                {
                    _currentMana -= 5;
                    _currentStamina -= 15;

                    Skill(0.45f);
                }
                // Character is moving
                else if (_horizontal != 0)
                {
                    _animator.SetInteger("_animState", 1);
                }
                // Character is standing still
                else
                {
                    _animator.SetInteger("_animState", 0);
                }
            }
            // In the air
            else
            {
                Debug.Log(_mainRB.velocity.y);
                _animator.SetInteger("_animState", 3);
                _animator.SetFloat("_velocityY", _mainRB.velocity.y);
                _mainRB.AddForce(Physics.gravity * 0.1f);
            }

            
        }
    }

    private void FixedUpdate()
    {
        // Movement
        if (_actionCooldown <= 0)
        {
            _mainRB.velocity = new Vector3(_horizontal * (_runSpeed * _actionSpeed), _mainRB.velocity.y, 0);         
        }
        else
        {
            _mainRB.velocity = new Vector3(0, _mainRB.velocity.y, 0);
        }
    }
}
