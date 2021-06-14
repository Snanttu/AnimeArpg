using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hitbox : MonoBehaviour
{
    [SerializeField]
    Attack_Object _owner;

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.tag == "Enemy" || collision.tag == "Player") && collision.tag != _owner.gameObject.tag)
        {
            Mortal_Object _target = collision.GetComponent<Mortal_Object>();
            _owner.DealHit(_target);
        }
    }
}
