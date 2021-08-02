using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_Boots", menuName = "ScriptableObjects/Armor_Boots", order = 4)]
public class Armor_Boots : Equipment_Base
{
    [SerializeField]
    private Sprite[] _bootsSprites;
    [SerializeField]
    private Sprite[] _masks;

    public Sprite GetSprite(int _index)
    {
        return _bootsSprites[_index];
    }
    public Sprite GetMask(int _index)
    {
        return _masks[_index];
    }
}
