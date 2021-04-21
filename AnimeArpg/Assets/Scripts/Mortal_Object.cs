using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An object that can take damage
public class Mortal_Object : MonoBehaviour
{
    [SerializeField]
    protected int _BaseHealth;
    [SerializeField]
    protected int _BaseMagic;

    protected int _currentHealth;
    protected int _maximumHealth;
    protected int _currentMagic;
    protected int _maximumMagic;

    // Defences         100 armour blocks 50% of 100 damage. Maximum mitigation 75%
    protected int _armour;
    protected int _magicArmour;

    // Resistances      extra damage mitigation % after armour mitigation. Maximum mitigation 50%
    protected int _bluntResistance;
    protected int _cutResistance;
    protected int _pierceResistance;

    protected int _fireResistance;
    protected int _lightningResistance;
    protected int _coldResistance;

    protected int _divineResistance;
    protected int _arcaneResistance;
    protected int _eldritchResistance;

    protected void Start()
    {
        _maximumHealth = _BaseHealth;
        _currentHealth = _maximumHealth;

        _maximumMagic = _BaseMagic;
        _currentMagic = _maximumMagic;
    }

    protected void Update()
    {
        // Health constraints
        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
        else if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        // Magic constraints
        if (_currentMagic > _maximumMagic)
        {
            _currentMagic = _maximumMagic;
        }
        else if (_currentMagic < 0)
        {
            _currentMagic = 0;
        }

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void GetHit(int _physDamage, int _magicDamage, int _magicLoss)
    {
        TakeDamage(_physDamage, _magicDamage);
        LoseMagic(_magicLoss);
    }

    protected void TakeDamage(int _physDamage, int _magicDamage)
    {
        float _physMitigation = _armour / _armour + _physDamage;
        // Maximum mitigation
        if (_physMitigation > 0.75)
        {
            _physMitigation = 0.75f;
        }

        float _magicMitigation = _magicArmour / _magicArmour + _magicDamage;
        // Maximum mitigation
        if (_magicMitigation > 0.75)
        {
            _magicMitigation = 0.75f;
        }

        int finalDamage = ((int) (_physDamage * _physMitigation) + (int) (_magicDamage * _magicMitigation));

        _currentHealth -= finalDamage;
    }

    protected void LoseMagic(int _amount)
    {
        _currentMagic -= _amount;
    }

    protected void Death()
    {
        Destroy(gameObject);
    }
}
