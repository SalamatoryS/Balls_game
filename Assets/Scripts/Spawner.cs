using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float _maxX = 2.5f;
    [SerializeField] GameObject _rayDown;

    float _oldX;
    float _x;

    private void Start()
    {
        _rayDown.SetActive(false);
    }

    private void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            _oldX = GetWorldMousePosition().x;
            _rayDown.SetActive(true);

        }
       if (Input.GetMouseButton(0))
        {
            float x = GetWorldMousePosition().x;
            float delta = x - _oldX;
            _oldX = x;
            _x += delta;
            _x = Mathf.Clamp(_x, -_maxX, _maxX);

            transform.position = new Vector3(_x, transform.position.y, 0f);
        }
       if(Input.GetMouseButtonUp(0)) 
        {
            _rayDown.SetActive(false);
        }
    }

    Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -_camera.transform.position.z;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
