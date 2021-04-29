using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delayed_Destroy : MonoBehaviour
{
    [SerializeField]
    float _delay;

    void Update()
    {
        Destroy(gameObject, _delay);
    }
}
