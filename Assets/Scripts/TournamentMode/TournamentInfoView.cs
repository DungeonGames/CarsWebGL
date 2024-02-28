using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Localization;
using UnityEngine.Serialization;

public class TournamentInfoView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private TextMeshProUGUI _timer;

    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private EnemySpawnerController _spawner;

    private bool _timerEnabled = false;

    private float _currentGameTime = 0;
    private int _score = 0;

    private void Start()
    {
        InitUI();

        _wavesManager.MinorWaveEnded += UpdateWaveNumber;
        _wavesManager.MajorWaveEnded += UpdateWaveNumber;

        _wavesManager.MinorWaveEnded += () => _timerEnabled = false;
        _wavesManager.MajorWaveEnded += () => _timerEnabled = false;

        _wavesManager.WaveStarted += () => _timerEnabled = true;

        _spawner.EnemyCountChanged += IncreaseScore;
    }

    private void IncreaseScore(int arg1, int arg2)
    {
        _score++;

        _killCountText.text = "Счёт: " + _score;
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
        _waveText.text = "Волна: 1";
        _killCountText.text = "Счёт: 0";
        _timer.text = "00:00";
    }

    private void UpdateWaveNumber()
    {
        _waveText.text = "Волна: " + _wavesManager.CurrentWave;
    }
}