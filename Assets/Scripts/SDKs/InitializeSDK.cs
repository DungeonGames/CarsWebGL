using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DungeonGames.VKGames;

public class InitializeSDK : MonoBehaviour
{
    public event Action Initialized;

    private void Awake()
    {
#if !CRAZY_GAMES
        StartCoroutine(Init());
#endif

#if CRAZY_GAMES
        SceneManager.LoadScene(1);
#endif
    }

    private void OnEnable()
    {
        Initialized += OnInitialized;
    }

    private void OnDisable()
    {
        Initialized -= OnInitialized;
    }

    private IEnumerator Init()
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();

#if !UNITY_WEBGL || UNITY_EDITOR
        yield return new WaitForSeconds(0.1f);

#elif YANDEX_GAMES

        yield return Agava.YandexGames.YandexGamesSdk.Initialize(Initialized);

#elif VK_GAMES
            
        yield return VKGamesSdk.Initialize(Initialized);
        
#endif

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene(1);
    }
}
