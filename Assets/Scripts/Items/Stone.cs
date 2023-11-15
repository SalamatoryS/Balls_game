using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] GameObject _dieEffect;
    int _level = 2;
    [SerializeField] Stone _stonePrefab;

    public override void Affect()
    {
        base.Affect();
        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildStone(_level - 1);
            }
        }
        else
        {
            ScoreManager.instance.AddScore(ItemType, transform.position);
        }
        Die();
    }

    void CreateChildStone(int level)
    {
        Stone newStone = (Instantiate(_stonePrefab, transform.position, Quaternion.identity));
        newStone.SetLevel(level);
    }

    private void SetLevel(int level)
    {
        float scale = 1f;
        if (level == 2)
        {
            scale = 1f;
        }
        else if (level == 1)
        {
            scale = 0.7f;
        }
        else if (level == 0)
        {
            scale = 0.45f;
        }
        transform.localScale = Vector3.one * scale;
        _level = level;
    }

    void Die()
    {
        if (_dieEffect != null)
            Instantiate(_dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
