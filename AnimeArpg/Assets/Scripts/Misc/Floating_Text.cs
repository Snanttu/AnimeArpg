using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floating_Text : MonoBehaviour
{
    [SerializeField]
    TextMeshPro _text;

    public void SetText(string _newText)
    {
        _text.text = _newText;
    }
}
