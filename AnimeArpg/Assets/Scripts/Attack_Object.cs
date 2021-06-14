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
    protected Collider2D[] _attackHitboxes;
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

            // Activate hitboxes during certain parts of an attack animation, each activation makes the hitbox active for 20% of the animation's duration
            if (_actionCooldown < (_actionActivation + _actionCooldown * 0.1f) || _actionCooldown < (_actionActivation - _actionCooldown * 0.1f))
            {
                _attackHitboxes[0].enabled = true;
            }
            else
            {
                _attackHitboxes[0].enabled = false;
            }
        }
        else
        {
            _attackHitboxes[0].enabled = false;
            _animator.speed = 1;
        }
    }

    protected void Skill(float _activation)
    {
        _actionCooldown = 1 / (_attackSpeed * _actionSpeed);
        _actionActivation = _actionCooldown * _activation;
        _animator.speed = _attackSpeed * _actionSpeed;
        _animator.SetInteger("_animState", 2);
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
