using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Box : PassiveItem
{
    public int health = 2;
    [SerializeField] GameObject[] _levels;
    [SerializeField] GameObject _breakEffectPrefab;
    [SerializeField] Animator _animator;

    private void OnValidate()
    {
        SetLevel(health);
    }
    public override void Affect()
    {
        base.Affect();
        health--;
        Instantiate(_breakEffectPrefab, transform.position, Quaternion.Euler(-90f, 0, 0));
        _animator.SetTrigger("Shake");
        if (health < 0)
        {
            Die();
        }
        else
        {
            SetLevel(health);
        }
    }

    private void SetLevel(int value)
    {
        for (int i = 0; i < _levels.Length; i++) 
        {
            _levels[i].SetActive(i <= value);
        }
    }

    private void Die()
    {
        ScoreManager.instance.AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}
