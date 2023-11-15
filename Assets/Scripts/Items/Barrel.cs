using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;
    public override void Affect()
    {
        base.Affect();
        if(_dieEffect != null)
            Instantiate(_dieEffect, transform.position, Quaternion.Euler(-90f,0f,0f));
        ScoreManager.instance.AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}
