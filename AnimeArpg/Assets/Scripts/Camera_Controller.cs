using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private int _minZoom;
    [SerializeField]
    private int _maxZoom;
    [SerializeField]
    private float _yPos;
    [SerializeField]
    private float _zoomPos;

    private Camera _camera;

    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (transform.position.z < _maxZoom)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (transform.position.z > _minZoom)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            }            
        }

        float _yZoom = transform.position.z * _zoomPos;

        if (_player != null)
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + _yZoom + _yPos, transform.position.z);
        }
    }
}
