using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
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
        VideoAd.Show(null, Success);
    }

    private void OnSuccessCallback()
    {
        _levelReward.InceaseCoinReward();
        _videoAdButton.interactable = false;
    }
}
