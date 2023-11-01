using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int level;
    public float radius;
    public bool active = true;

    [SerializeField] Transform _visualTransform;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] SphereCollider _collider;
    [SerializeField] SphereCollider _trigger;
    [SerializeField] Renderer _renderer;
    [SerializeField] BallSettings _ballSettings;
    [SerializeField] Rigidbody _rigidbody;

    CollapseManager _collapseManager;

    [ContextMenu("IncreaseLevel")]
    public void IncreaseLever()
    {
        level++;
        SetLevel(level);
        _trigger.enabled = false;
        _trigger.enabled = true;
    }

    public void SetLevel(int lvl)
    {
        level = lvl;
        int number = (int)Mathf.Pow(2,lvl+1);
        _text.text = number.ToString();

        radius = Mathf.Lerp(0.4f, 0.7f, lvl / 10f);
        Vector3 ballScale = Vector3.one * radius * 2f;
        _visualTransform.localScale = ballScale;
        _collider.radius = radius;
        _trigger.radius = radius + 0.1f;

        _renderer.material = _ballSettings.ballMaterials[level];
    }

    public void Init(CollapseManager collapseManager)
    {
        _collapseManager = collapseManager;
    }

    public void SetToTube()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
        _rigidbody.velocity = Vector3.down * 1.2f;
    }

    public void Deactivate()
    {
        active = false;
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.attachedRigidbody)
            {
                if (other.attachedRigidbody.GetComponent<Ball>() is Ball otherBall)
                {
                    if (otherBall.level == level && otherBall.active)
                    {
                        _collapseManager.Collapse(this, otherBall);
                    }
                }
            }
        }
    }
}
