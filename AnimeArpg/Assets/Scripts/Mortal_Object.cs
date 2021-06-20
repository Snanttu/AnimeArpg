using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An object that can take damage
public class Mortal_Object : MonoBehaviour
{
    [SerializeField]
    protected int _baseHealth = 100;
    [SerializeField]
    protected float _baseHealthRegen = 0;
    [SerializeField]
    protected int _baseMana = 100;
    [SerializeField]
    protected float _baseManaRegen = 0.05f;
    [SerializeField]
    protected int _baseStamina = 100;
    [SerializeField]
    protected float _baseStaminaRegen = 0.15f;
    [SerializeField]
    protected int _baseArmour = 0;
    [SerializeField]
    protected int _baseMagicArmour = 0;

    protected Rigidbody _mainRB;
    protected Animator _animator;

    [SerializeField]
    private GameObject _damageText;
    [SerializeField]
    private Transform _textLocation;
    [SerializeField]
    private Transform _center;

    private float _staggerCD = 0.1f;
    private float _staggerLeft = 0;
    protected int _currentHealth;
    protected int _maximumHealth;    
    private float _healthRegen = 0;
    protected int _currentMana;
    protected int _maximumMana;    
    private float _manaRegen = 0;
    protected int _currentStamina;
    protected int _maximumStamina;
    
    private float _staminaRegen = 0;

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

    //private float _second;
    //private int _somethingPerSecond = 0;

    protected void Start()
    {
        _mainRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        // Set base stats
        _maximumHealth = _baseHealth;
        _currentHealth = _maximumHealth;

        _maximumMana = _baseMana;
        _currentMana = _maximumMana;

        _maximumStamina = _baseStamina;
        _currentStamina = _maximumStamina;

        _armour = _baseArmour;
        _magicArmour = _baseMagicArmour;
    }

    protected void Update()
    {
        /*
        if (gameObject.tag == "Player")
        {
            if (_second > 0)
            {
                _second -= Time.deltaTime;
            }
            else
            {
                Debug.Log(_somethingPerSecond);
                _second = 1;
                _somethingPerSecond = 0;
            }
        }  
        */

        if (_staggerLeft > 0)
        {
            _staggerLeft -= Time.deltaTime;
        }
        else
        {
            _animator.SetFloat("_damage", 0);
        }


        Regen();

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
        if (_currentMana > _maximumMana)
        {
            _currentMana = _maximumMana;
        }
        else if (_currentMana < 0)
        {
            _currentMana = 0;
        }

        // Stamina constraints
        if (_currentStamina > _maximumStamina)
        {
            _currentStamina = _maximumStamina;
        }
        else if (_currentStamina < 0)
        {
            _currentStamina = 0;
        }

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    protected void CallAnimation(float _new)
    {      
        _animator.SetInteger("_animState", (int)_new);
    }

    public void Regen()
    {
        float _healthRegenPercentage = ((float)_maximumHealth * _baseHealthRegen) * Time.deltaTime;
        _healthRegen += _healthRegenPercentage;
        int _addHealth = 0;

        if (_healthRegen > 1)
        {            
            _addHealth = (int)(_healthRegen);
            _healthRegen -= _addHealth;
        }

        _currentHealth += _addHealth;

        float _manaRegenPercentage = ((float)_maximumMana * _baseManaRegen) * Time.deltaTime;
        _manaRegen += _manaRegenPercentage;
        int _addMana = 0;

        if (_manaRegen > 1)
        {
            _addMana = (int)(_manaRegen);
            _manaRegen -= _addMana;
        }

        _currentMana += _addMana;

        float _staminaRegenPercentage = ((float)_maximumStamina * _baseStaminaRegen) * Time.deltaTime;
        _staminaRegen += _staminaRegenPercentage;
        int _addStamina = 0;

        if (_staminaRegen > 1)
        {
            _addStamina = (int)(_staminaRegen);
            _staminaRegen -= _addStamina;
        }

        _currentStamina += _addStamina;
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
            GameObject _text = Instantiate(_damageText, _textLocation.position, Quaternion.identity);
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
        _currentMana -= _amount;
    }

    protected void Death()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Mortal_Object>().enabled = false;
        _animator.speed = 1;
        _mainRB.velocity = new Vector3(0, 0, 0);
        _animator.SetBool("_death", true);
        gameObject.tag = "Corpse";        
    }

    public int GetMaxHealth()
    {
        return _maximumHealth;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public int GetMaxMana()
    {
        return _maximumMana;
    }

    public int GetMana()
    {
        return _currentMana;
    }

    public int GetMaxStamina()
    {
        return _maximumStamina;
    }

    public int GetStamina()
    {
        return _currentStamina;
    }
}
