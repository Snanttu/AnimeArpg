using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    Mortal_Object _owner;
    [SerializeField]
    Transform _bar;
    [SerializeField]
    Transform _health;

    // Update is called once per frame
    void Update()
    {
        if (_owner.GetHealth() == _owner.GetMaxHealth() || _owner.GetHealth() <= 0)
        {
            _bar.GetComponent<SpriteRenderer>().enabled = false;
            _health.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            _bar.GetComponent<SpriteRenderer>().enabled = true;
            _health.GetComponent<SpriteRenderer>().enabled = true;

            float _hpPercentage = (float)_owner.GetHealth() / (float)_owner.GetMaxHealth() * 100;

            _health.transform.localScale = new Vector3(_hpPercentage, 10, 1);
        }        
    }
}
