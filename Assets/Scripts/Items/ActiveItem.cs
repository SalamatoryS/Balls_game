using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveItem : Item
{
    public int level;
    public bool active = true;
    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;
    [SerializeField] protected Rigidbody _rigidbody;
    protected CollapseManager _collapseManager;
    
    protected int numberToWin;


    protected virtual void OnValidate()
    {
        SetLevel(level);
    }

    public void Init(CollapseManager collapseManager)
    {
        _collapseManager = collapseManager;
    }

    public virtual void SetLevel(int lvl)
    {
        level = lvl;
        int number = (int)Mathf.Pow(2, lvl + 1);
        _text.text = number.ToString();
        numberToWin = number;
    }

    public void Deactivate()
    {
        active = false;
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public virtual void DoEffect()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.attachedRigidbody)
            {
                if (other.attachedRigidbody.GetComponent<ActiveItem>() is ActiveItem otherActiveItem)
                {
                    if (otherActiveItem.level == level && otherActiveItem.active)
                    {
                        _collapseManager.Collapse(this, otherActiveItem);
                    }
                }
            }
        }
    }
}
