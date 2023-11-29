using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] string _textEn;
    [SerializeField] string _textRu;

    private void Start()
    {
        if (LanguageManager.Instance.currentLanguage == "en")
        {
            GetComponent<TMP_Text>().text = _textEn;
        }
        else
        {
            GetComponent<TMP_Text>().text = _textRu;
        }
    }
}
