using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Melee_Enemy : Attack_Object
{
    [SerializeField]
    private float _runSpeed = 0.5f;

    protected GameObject _player;
    protected Vector3 _targetPosition;
    protected float _distanceFromTarget;

    private int _direction = 1;
    private bool _grounded;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        try
        {
            _player = GameObject.FindWithTag("Player");
        }
        catch(NullReferenceException)
        {
            _player = null;
        }

        if (_player != null)
        {
            _targetPosition = _player.transform.position;

            _direction = (_targetPosition.x < transform.position.x) ? -1 : 1;

            _distanceFromTarget = Vector2.Distance(transform.position, _targetPosition);
        }

        // Ground check
        Collider[] _groundCheck = Physics.OverlapBox(gameObject.transform.position, new Vector3(1, 0.2f, 0.2f), Quaternion.identity, LayerMask.GetMask("Terrain"));

        if (_groundCheck.Length > 0)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }

        // On the ground
        if (_grounded)
        {
            if (_actionCooldown <= 0)
            {
                Vector3 _box = new Vector3(transform.position.x + _direction, transform.position.y + 2, transform.position.z);
                Collider[] _playerCheck = Physics.OverlapBox(_box, new Vector3(1, 2, 1), transform.rotation, _enemies);

                // Character is performing a skill
                if (_playerCheck.Length > 0)
                {
                    Vector3 targetPos = _playerCheck[0].transform.position;

                    // Rotate character towards movement
                    if (targetPos.x > transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    Skill(0.45f);
                }
                // Character is moving
                else if (_mainRB.velocity.x != 0)
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
        // In the air
        else
        {
            Debug.Log(_mainRB.velocity.y);
            _animator.SetInteger("_animState", 3);
            _animator.SetFloat("_velocityY", _mainRB.velocity.y);
            _mainRB.AddForce(Physics.gravity * 0.1f);
        }

        
    }

    private void FixedUpdate()
    {
        if (_actionCooldown <= 0)
        {
            if (_player != null && _grounded)
            {
                float _directionX = _targetPosition.x - transform.position.x;

                _mainRB.velocity = new Vector3(_directionX, 0, 0).normalized * (_runSpeed * _actionSpeed);                

                // Rotate character towards movement
                if (_directionX > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
        else
        {
            _mainRB.velocity = new Vector3(0, 0, 0);
        }
    }
}
