using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamit : ActiveItem
{
    [SerializeField] float _affectRadius = 1.5f;
    [SerializeField] float _forceValue = 500f;
    [SerializeField] GameObject _affectArea;
    [SerializeField] GameObject _affectPrefab;
    [SerializeField] Animator _animator;

    private void Start()
    {
        _affectArea.SetActive(false);
    }

    IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere (transform.position, _affectRadius);
        for (int i = 1; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                Vector3 to = (rigidbody.transform.position - transform.position).normalized;
                rigidbody.AddForce(to * _forceValue + Vector3.up * _forceValue * 0.5f);

                PassiveItem passiveItem = rigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                {
                    passiveItem.Affect();
                }
            }
        }
        if(_affectPrefab != null)
            Instantiate(_affectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

}
