using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : Mortal_Object
{
    private Rigidbody _mainRB;

    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;

    [SerializeField]
    private float _runSpeed = 1;

    new void Start()
    {
        base.Start();
        _mainRB = GetComponent<Rigidbody>();
    }

    new void Update()
    {
        base.Update();
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");        
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
