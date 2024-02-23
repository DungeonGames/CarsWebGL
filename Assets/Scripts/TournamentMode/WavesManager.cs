using System;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private int _gameStageWavesCount;

    private int _currentWave = 1;

    public int CurrentWave => _currentWave;

    public Action MinorWaveEnded;
    public Action MajorWaveEnded;

    private void FinishWave()
    {
        if (_currentWave % _gameStageWavesCount == 0)
        {
            MajorWaveEnded?.Invoke();
        }
        else
        {
            MinorWaveEnded?.Invoke();
        }

        _currentWave++;
    }
}
