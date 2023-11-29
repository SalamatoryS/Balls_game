using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneLoader : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Load), 3f);
    }

    void Load()
    {
        SceneManager.LoadScene(1);
    }
}
