using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class ProgressData
{
    public int level;
    public int completedLevels = 0;
}

public class Progress : MonoBehaviour
{
    public int level;

    public int completeLevels;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevel(int level)
    {
        this.level = level;
        Save();
    }

    public void IncreaseCompletedLevels()
    {
        completeLevels++;
    }

    void Save()
    {
        ProgressData data = new ProgressData();
        data.level = level;
        data.completedLevels = completeLevels;
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Progress", json);
    }

    void Load()
    {
        if (PlayerPrefs.HasKey("Progress"))
        {
            string json = PlayerPrefs.GetString("Progress");
            ProgressData data = JsonUtility.FromJson<ProgressData>(json);
            level = data.level;
            completeLevels = data.completedLevels;
        }
    }
}
