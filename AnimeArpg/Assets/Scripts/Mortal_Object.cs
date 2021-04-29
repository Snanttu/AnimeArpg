using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An object that can take damage
public class Mortal_Object : MonoBehaviour
{
    [SerializeField]
    protected int _baseHealth;
    [SerializeField]
    protected int _baseMagic;
    [SerializeField]
    protected int _baseArmour;
    [SerializeField]
    protected int _baseMagicArmour;

    protected Rigidbody _mainRB;
    protected Animator _animator;

    [SerializeField]
    private GameObject _damageText;
    [SerializeField]
    private Transform _damageLocation;
    [SerializeField]
    private Transform _center;

    private float _staggerCD = 0.5f;
    private float _staggerLeft = 0;

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
        _mainRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        // Set base stats
        _maximumHealth = _baseHealth;
        _currentHealth = _maximumHealth;

        _maximumMagic = _baseMagic;
        _currentMagic = _maximumMagic;

        _armour = _baseArmour;
        _magicArmour = _baseMagicArmour;
    }

    protected void Update()
    {
        if (_staggerLeft > 0)
        {
            _staggerLeft -= Time.deltaTime;
        }
        else
        {
            _animator.SetFloat("_damage", 0);
        }

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

    public void GetHit(int _physDamage, int _magicDamage, int _magicLoss, GameObject _attacker, GameObject _hitEffect)
    {
        TakeDamage(_physDamage, _magicDamage, _attacker, _hitEffect);
        LoseMagic(_magicLoss);
    }

    protected void TakeDamage(int _physDamage, int _magicDamage, GameObject _attacker, GameObject _hitEffect)
    {
        float _physMitigation = 1;
        if (_armour > 0)
        {
            _physMitigation = (float)_armour / (_armour + _physDamage);
            // Maximum mitigation
            if (_physMitigation > 0.75)
            {
                _physMitigation = 0.75f;
            }
            _physMitigation = 1 - _physMitigation;
        }

        float _magicMitigation = 1;
        if (_magicArmour > 0)
        {
            _magicMitigation = (float)_magicArmour / (_magicArmour + _magicDamage);
            // Maximum mitigation
            if (_magicMitigation > 0.75)
            {
                _magicMitigation = 0.75f;
            }
            _magicMitigation = 1 - _magicMitigation;
        }        

        int finalDamage = ((int) (_physDamage * _physMitigation) + (int) (_magicDamage * _magicMitigation));
        Instantiate(_hitEffect, _center.position, _center.rotation);
        _currentHealth -= finalDamage;

        if (_damageText != null)
        {
            GameObject _text = Instantiate(_damageText, _damageLocation.position, Quaternion.identity);
            Floating_Text _textScript = _text.GetComponent<Floating_Text>();
            _textScript.SetText(finalDamage.ToString());
        }

        if (_staggerLeft <= 0)
        {
            _staggerLeft = _staggerCD;
            _animator.SetFloat("_damage", 1);
        }
    }

    protected void LoseMagic(int _amount)
    {
        _currentMagic -= _amount;
    }

    protected void Death()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Mortal_Object>().enabled = false;
        _animator.SetBool("_death", true);
    }

    public int GetMaxHealth()
    {
        return _maximumHealth;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }
}
