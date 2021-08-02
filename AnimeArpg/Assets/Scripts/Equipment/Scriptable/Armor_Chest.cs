using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_Chest", menuName = "ScriptableObjects/Armor_Chest", order = 1)]
public class Armor_Chest : Equipment_Base
{  
    [SerializeField]
    private Sprite[] _shirtSprites;
    [SerializeField]
    private Sprite[] _skirtSprites;
    [SerializeField]
    private Sprite[] _shoulderSprites;

    [SerializeField]
    private Sprite[] _masks;

    public Sprite GetChestSprite (int _index)
    {
        return _shirtSprites[_index];
    }
    public Sprite GetMask(int _index)
    {
        return _masks[_index];
    }

    public Sprite GetSkirtSprite(int _index)
    {
        return _skirtSprites[_index];
    }

    public Sprite GetShoulderSprite(int _index)
    {
        return _shoulderSprites[_index];
    }
}