using System;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    private const string CountOfRestartedSceneKey = "CountOfRestartedScene";
    
    private int _countOfRestartedScene = 0;

    private void Awake()
    {
        _countOfRestartedScene = PlayerPrefs.GetInt(CountOfRestartedSceneKey, 0);
    }

    public void LoadScene()
    {
        _countOfRestartedScene++;
        PlayerPrefs.SetInt(CountOfRestartedSceneKey, _countOfRestartedScene);
        PlayerPrefs.Save();
        GameAnalytics.NewDesignEvent("CountOfRestartedScene", _countOfRestartedScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
