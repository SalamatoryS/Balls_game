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
        _levelText.text ="Уровень " + (Progress.Instance.level + 1).ToString();
    }
    public void StartLevel()
    {
        int level = Progress.Instance.level;
        SceneManager.LoadScene(level + 1);
    }
}
