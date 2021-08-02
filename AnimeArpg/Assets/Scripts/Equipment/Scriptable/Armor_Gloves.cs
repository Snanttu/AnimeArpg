using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_Gloves", menuName = "ScriptableObjects/Armor_Gloves", order = 3)]
public class Armor_Gloves : Equipment_Base
{
    [SerializeField]
    private Sprite[] _glovesSprites;
    [SerializeField]
    private Sprite[] _masks;
    [SerializeField]
    private Sprite[] _handSprites;

    public Sprite GetGlovesSprite(int _index)
    {
        return _glovesSprites[_index];
    }
    public Sprite GetMask(int _index)
    {
        return _masks[_index];
    }

    public Sprite GetHandsSprite(int _index)
    {
        return _handSprites[_index];
    }
}
