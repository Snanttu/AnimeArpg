using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player character
public class Player_Controller : Attack_Object
{
    [SerializeField]
    private float _runSpeed = 1;
    [SerializeField]
    private LayerMask _targets;

    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;

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
            // Character is performing a skill
            if (Input.GetButton("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit Hit;
                Vector3 targetPos;

                if (Physics.Raycast(ray, out Hit, 100, _targets))
                {
                    targetPos = Hit.point;

                    if (Hit.collider.gameObject.tag == "Enemy" || Hit.collider.gameObject.tag == "Breakable")
                    {
                        targetPos = Hit.collider.gameObject.transform.position;
                    }

                    targetPos -= transform.position;
                    targetPos.y = 0;

                    transform.rotation = Quaternion.LookRotation(targetPos);

                    _currentMana -= 5;
                    _currentStamina -= 15;

                    Skill(0.45f);
                }                
            }
            // Character is running
            else if (_horizontal != 0 || _vertical != 0)
            {
                // Rotate character towards movement
                float singleStep = 10f * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(_horizontal, 0.0f, _vertical), singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);

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
            if (_horizontal != 0 && _vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, otherwise the player would move faster diagonally
                _horizontal *= _moveLimiter;
                _vertical *= _moveLimiter;
            }

            _mainRB.velocity = new Vector3(_horizontal * (_runSpeed * _actionSpeed), 0, _vertical * (_runSpeed * _actionSpeed));
        }
        else
        {
            _mainRB.velocity = new Vector3(0, 0, 0);
        }
    }
}
