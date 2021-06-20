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

    protected float _actionSpeed = 1;
    protected float _actionActivation;
    protected float _actionCooldown;

    new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();

        if (_actionCooldown > 0)
        {
            _actionCooldown -= Time.deltaTime;
        }
        else
        {
            _animator.speed = 1;
        }
    }

    protected void Skill(float _activation)
    {
        _actionCooldown = 1 / (_attackSpeed * _actionSpeed);
        _actionActivation = _actionCooldown * _activation;
        _animator.speed = _attackSpeed * _actionSpeed;

        CallAnimation(2);
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
