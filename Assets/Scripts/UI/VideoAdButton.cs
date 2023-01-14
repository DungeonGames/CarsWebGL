using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VideoAdButton : MonoBehaviour
{
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private AudioResources _audioResources;
    [SerializeField] private Button _videoAdButton;

    public event Action Success;

    public void ShowVideoAd()
    {
#if YANDEX_GAMES
        Agava.YandexGames.VideoAd.Show(OnOpenVideo, OmRewarded, OnClose);
        
#endif

#if VK_GAMES
        Agava.VKGames.VideoAd.Show(OnVKCallback);
        OnOpenVideo();
#endif
    }

    private void OnOpenVideo()
    {
        _audioResources.Mute();
    }

    private void OmRewarded()
    {
        _levelReward.InceaseCoinReward();
        _videoAdButton.interactable = false;
    }

    private void OnClose()
    {
        _audioResources.UnMute();
    }

    private void OnVKCallback()
    {
        _levelReward.InceaseCoinReward();
        _videoAdButton.interactable = false;
        OnClose();
    }
}
