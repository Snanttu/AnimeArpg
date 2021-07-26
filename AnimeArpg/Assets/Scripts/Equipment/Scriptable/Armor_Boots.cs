using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_Boots", menuName = "ScriptableObjects/Armor_Boots", order = 4)]
public class Armor_Boots : Equipment_Base
{
    [SerializeField]
    private Sprite[] _bootsSprites;

    public Sprite GetSprite(int _index)
    {
        return _bootsSprites[_index];
    }
}
