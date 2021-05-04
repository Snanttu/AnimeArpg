using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Object : Attack_Object
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (_actionCooldown <= 0)
        {
            Vector3 _box = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Collider[] _playerCheck = Physics.OverlapBox(_box, new Vector3(1, 1, 1), transform.rotation, _enemies);

            // Character is performing a skill
            if (_playerCheck.Length > 0)
            {
                Debug.Log(_playerCheck[0]);
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
}
