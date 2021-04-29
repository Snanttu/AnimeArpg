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
    protected float _attackSpeed;

    [SerializeField]
    protected GameObject _hitEffect;
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
        _target.GetHit(DamageRoll(_basePhysicalAttack), DamageRoll(_baseMagicAttack), 0, gameObject, _hitEffect);
    }

    private int DamageRoll(int _damage)
    {
        int _minDamage = (int)(_damage * 0.8f);
        int _maxDamage = (int)(_damage * 1.2f);

        return Random.Range(_minDamage, _maxDamage);
    }

    public LayerMask GetTargets()
    {
        return _enemies;
    }
}
