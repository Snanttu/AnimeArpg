using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Base : ScriptableObject
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected string _description;

    protected void Start()
    {
        
    }

    public Sprite GetSprite(Sprite _sprite)
    {
        return _sprite;
    }
}
