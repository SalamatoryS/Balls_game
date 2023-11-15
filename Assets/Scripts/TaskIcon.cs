using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskIcon : MonoBehaviour
{
    public ItemType itemType;
    public int currentScore;

    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;
    [SerializeField] ItemIcons _itemIcons;
    [SerializeField] AnimationCurve _scaleCurve;

    public void Setup(ItemType itemType, int number)
    {
        this.itemType = itemType;
        currentScore = number;
        _image.sprite = _itemIcons.GetSprite(itemType);
        _text.text = number.ToString();
    }
    
    public void AddOne()
    {
        currentScore--;
        if(currentScore < 0)
        {
            currentScore = 0;
        }
        _text.text = currentScore.ToString();
        StartCoroutine(AddAnimation());
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            float scale =_scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
