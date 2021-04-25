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
    [SerializeField]
    protected LayerMask _enemies;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    public void DealHit(Mortal_Object _target)
    {
        _target.GetHit(_basePhysicalAttack, _baseMagicAttack, 0, gameObject);
    }

    public LayerMask GetTargets()
    {
        return _enemies;
    }
}
