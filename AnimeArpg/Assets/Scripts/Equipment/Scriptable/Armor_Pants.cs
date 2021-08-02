using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_Pants", menuName = "ScriptableObjects/Armor_Pants", order = 2)]
public class Armor_Pants : Equipment_Base
{
    [SerializeField]
    private Sprite[] _pantsSprites;
    [SerializeField]
    private Sprite[] _masks;

    public Sprite GetSprite(int _index)
    {
        return _pantsSprites[_index];
    }
    public Sprite GetMask(int _index)
    {
        return _masks[_index];
    }
}
