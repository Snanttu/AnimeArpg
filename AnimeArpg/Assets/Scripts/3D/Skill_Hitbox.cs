using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hitbox : MonoBehaviour
{
    [SerializeField]
    Attack_Object _owner;

    private void OnTriggerEnter(Collider other)
    {

        if (_owner.tag != other.tag)
        {
            Mortal_Object _target = other.GetComponent<Mortal_Object>();
            _owner.DealHit(_target);
        }
    }
}
