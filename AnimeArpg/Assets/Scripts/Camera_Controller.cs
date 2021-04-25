using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _yPos;
    [SerializeField]
    private float _zPos;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + _yPos, _player.transform.position.z + _zPos); ;
    }
}
