using Agava.YandexGames;
using System;
using System.Collections;
using IJunior.TypedScenes;
using UnityEngine;

public class InitializeSDK : MonoBehaviour
{
    public event Action Initialized;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize(Initialized);      
    }

    private void OnEnable()
    {
        Initialized += ChangeScene;
    }

    private void OnDisable()
    {
        Initialized -= ChangeScene;
    }

    private void ChangeScene()
    {
        GameScene.Load();
    }
}
