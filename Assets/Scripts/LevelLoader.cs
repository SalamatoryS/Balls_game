using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    private void Start()
    {
        SetLevelInMenu();
    }
    public void StartLevel()
    {
        int level = Progress.Instance.level;
        SceneManager.LoadScene(level + 2);
    }

    public void ResetProgress()
    {
        Progress.Instance.SetLevel(0);
        SetLevelInMenu();
    }

    void SetLevelInMenu()
    {
        string level;
        if (LanguageManager.Instance.currentLanguage == "en")
        {
            level = "LEVEL ";
        }
        else
        {
            level = "спнбемэ ";
        }
        _levelText.text = level + (Progress.Instance.level + 1).ToString();
    }
}
