using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VideoAdButton : MonoBehaviour
{
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private Button _videoAdButton;

    public event Action Success;

    private void OnEnable()
    {
        Success += OnSuccessCallback;
    }

    private void OnDisable()
    {
        Success -= OnSuccessCallback;
    }

    public void ShowVideoAd()
    {
#if YANDEX_GAMES
        Agava.YandexGames.VideoAd.Show(null, Success);
#endif
#if VK_GAMES
        Agava.VKGames.VideoAd.Show(Success);
#endif
    }

    private void OnSuccessCallback()
    {
        _levelReward.InceaseCoinReward();
        _videoAdButton.interactable = false;
    }
}
