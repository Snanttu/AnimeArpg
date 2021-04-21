using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An object that can deal damage
public class Attack_Object : Mortal_Object
{
    [SerializeField]
    protected int _basePhysicalAttack;
    [SerializeField]
    protected int _baseMagicAttack;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    protected void DealHit(int _physDamage, int _magicDamage, int _magicLoss, Mortal_Object _target)
    {
        _target.GetHit(_physDamage, _magicDamage, _magicLoss);
    }
}
