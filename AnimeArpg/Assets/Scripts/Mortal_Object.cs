using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void GetHit(int _damage, int _magicLoss)
    {
        TakeDamage(_damage);
        LoseMagic(_magicLoss);
    }

    protected void TakeDamage(int _amount)
    {
        _currentHealth -= _amount;
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
