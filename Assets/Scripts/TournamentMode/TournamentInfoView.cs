using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using TMPro;
using Lean.Localization;
using PlayDeck;
using UnityEngine.Serialization;

public class TournamentInfoView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private GameUIHandler _gameUIHandler;

    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private EnemySpawnerController _spawner;
    [SerializeField] private PlayDeckBridge _playDeckBridge;

    private const string _scoreToken = "Score";
    private const string _waveToken = "Wave";

    private bool _timerEnabled = false;

    private float _currentGameTime = 0;
    private int _score = 0;
    private int _previousScore;

    private void Start()
    {
        InitUI();

        _wavesManager.MinorWaveEnded += UpdateWaveNumber;
        _wavesManager.MajorWaveEnded += UpdateWaveNumber;

        _wavesManager.MinorWaveEnded += () => _timerEnabled = false;
        _wavesManager.MajorWaveEnded += () => _timerEnabled = false;

        _wavesManager.WaveStarted += () => _timerEnabled = true;

        _spawner.EnemyCountChanged += IncreaseScore;
        _gameUIHandler.GameEnd += OnGameEnded;

        _playDeckBridge.GetScore(SetPreviousScore);
    }

    private void SetPreviousScore(PlayDeckBridge.GetScoreData data) =>
        _previousScore = data.score;

    private void IncreaseScore(int arg1, int arg2)
    {
        _score++;
        
        if (_score > _previousScore)
            _playDeckBridge.SetScore(_score);
        
        GameAnalytics.NewDesignEvent("EnemyKilled", _score);

        _killCountText.text = $"{LeanLocalization.GetTranslationText(_scoreToken)}: " + _score;
    }

    private void Update()
    {
        if (_timerEnabled)
        {
            CountTime();
        }
    }

    private void CountTime()
    {
        _currentGameTime += Time.deltaTime;

        TimeSpan sessionTime = TimeSpan.FromSeconds(_currentGameTime);
        _timer.text = sessionTime.ToString(@"mm\:ss");
    }

    private void InitUI()
    {
        _waveText.text = $"{LeanLocalization.GetTranslationText(_waveToken)}: 1";
        _killCountText.text = $"{LeanLocalization.GetTranslationText(_scoreToken)}: 0";
        _timer.text = "00:00";
    }

    private void UpdateWaveNumber()
    {
        _waveText.text = $"{LeanLocalization.GetTranslationText(_waveToken)}: " + _wavesManager.CurrentWave;
    }

    private void OnGameEnded()
    {
        GameAnalytics.NewDesignEvent("CountOfWavesThenPlayerDied", _wavesManager.CurrentWave);
    }
}