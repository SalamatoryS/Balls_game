using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : ActiveItem
{
    

    public float radius;
    [SerializeField] Transform _visualTransform;
    [SerializeField] Renderer _renderer;
    [SerializeField] BallSettings _ballSettings;
   
    [ContextMenu("IncreaseLevel")]
    public void IncreaseLevel()
    {
        level++;
        SetLevel(level);
        _trigger.enabled = false;
        _trigger.enabled = true;
    }

    public override void SetLevel(int lvl)
    {
        base.SetLevel(lvl);

        radius = Mathf.Lerp(0.4f, 0.7f, lvl / 10f);
        Vector3 ballScale = Vector3.one * radius * 2f;
        _visualTransform.localScale = ballScale;
        _collider.radius = radius;
        _trigger.radius = radius + 0.1f;

        _renderer.material = _ballSettings.ballMaterials[level];
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

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
        AffectPassiveItems(transform.position, radius);
    }
    private void AffectPassiveItems(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius + 0.15f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                PassiveItem passiveItem = colliders[i].attachedRigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                {
                    passiveItem.Affect();
                }
            }
        }
    }


}
