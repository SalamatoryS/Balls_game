using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdv();

    [DllImport("__Internal")]
    private static extern void SetLeaderboardScores(string nameLB, int score);

    [SerializeField] GameObject _winWindow;
    [SerializeField] GameObject _loseWindow;
    
    public void Win()
    {
        _winWindow.SetActive(true);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Progress.Instance.IncreaseCompletedLevels();
        Progress.Instance.SetLevel(currentSceneIndex - 1);
    }

    public void Lose()
    {
        _loseWindow.SetActive(true);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        SetLeaderboardScores("LeadBoard",Progress.Instance.completeLevels);
        SceneManager.LoadScene(Progress.Instance.level + 2);
        ShowFullscreenAdv();
    }
}
