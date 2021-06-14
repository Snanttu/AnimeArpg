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
            _distanceFromTarget = Vector2.Distance(transform.position, _targetPosition);
        }

        if (_actionCooldown <= 0)
        {       
            Vector3 _box = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Collider[] _playerCheck = Physics.OverlapBox(_box, new Vector3(1, 1, 1), transform.rotation, _enemies);

            // Character is performing a skill
            if (_playerCheck.Length > 0)
            {
                Vector3 targetPos = _playerCheck[0].transform.position;

                targetPos -= transform.position;
                targetPos.y = 0;

                transform.rotation = Quaternion.LookRotation(targetPos);

                Skill(0.45f);
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
        if (_actionCooldown <= 0)
        {
            if (_player != null)
            {
                float _directionX = _targetPosition.x - transform.position.x;
                float _directionZ = _targetPosition.z - transform.position.z;

                _mainRB.velocity = new Vector3(_directionX, 0, _directionZ).normalized * (_runSpeed * _actionSpeed);

                // Rotate character towards movement
                float singleStep = 10f * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(_directionX, 0.0f, _directionZ), singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else
            {
                _mainRB.velocity = new Vector3(0, 0, 0);
            }
        }        
    }
}
