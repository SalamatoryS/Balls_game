using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayDown : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Transform _pointer;
    [SerializeField] Transform _ray;
    [SerializeField] Renderer _renderer;
    [SerializeField] BallSettings _ballSettings;
    [SerializeField] TextMeshProUGUI _text;
    float _radius = 0.5f;
    float _distance = 20f;
    Vector3 _rayStable;

    private void Start()
    {
        _rayStable = _ray.transform.localScale - Vector3.up * 2f; 
    }
    private void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.SphereCast(ray, _radius, out hit, _distance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _pointer.position = transform.position + Vector3.down * hit.distance;
            _ray.position = (transform.position + (Vector3.down * hit.distance / 2f));
            _ray.transform.localScale = _rayStable + Vector3.up * hit.distance;
        }
    }

    public void SetData(int lvl,float rad)
    {
        _renderer.material = _ballSettings.rayPointMaterials[lvl];
        _radius = rad;
        rad *= 2f;
        _pointer.transform.localScale = new Vector3(rad, rad, rad);
        
        int number = (int)Mathf.Pow(2, lvl + 1);
        _text.text = number.ToString();
    }


}
