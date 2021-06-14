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
    private LayerMask _targets;

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

            // Character is performing a skill
            if (Input.GetButtonDown("Fire1"))
            {
                _currentMana -= 5;
                _currentStamina -= 15;

                Skill(0.45f);
            }
            // Jump
            else if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            {
                Vector2 jump = new Vector2(0.0f, 2.0f);
                _mainRB.AddForce(jump * _jumpForce, ForceMode2D.Impulse);
                _grounded = false;
            }
            // In the Air
            else if (_mainRB.velocity.y > 0 && !_grounded)
            {
                _animator.SetInteger("_animState", 3);
            }
            // Character is running
            else if (_horizontal != 0 && _grounded)
            {   
                _animator.SetInteger("_animState", 1);
            }
            // Character is standing still
            else
            {
                _animator.SetInteger("_animState", 0);
            }
        }
    }

    private void FixedUpdate()
    {
        // Cannot move during an action
        if (_actionCooldown <= 0)
        {
            _mainRB.velocity = new Vector2(_horizontal * (_runSpeed * _actionSpeed), _mainRB.velocity.y);
        }
        else
        {
            _mainRB.velocity = new Vector2(0, _mainRB.velocity.y);
        }
    }

    public void SetGrounded(bool _bool)
    {
        _grounded = _bool;
    }
}
