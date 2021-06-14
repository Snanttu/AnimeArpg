using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Checker : MonoBehaviour
{
    [SerializeField]
    Player_Controller _owner;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            _owner.SetGrounded(true);
        }
    }
}
