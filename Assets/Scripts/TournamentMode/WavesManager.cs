using System;
using System.Collections;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private int _gameStageWavesCount;
    [SerializeField] private EnemySpawnerController _enemySpawner;

    private int _currentWave = 1;

    public int CurrentWave => _currentWave;
    public int BigStage { get; private set; }

    public Action WaveStarted;
    
    public Action MinorWaveEnded;
    public Action MajorWaveEnded;

    private void Start()
    {
        BigStage = 1;
        StartWave();

        _enemySpawner.MapClear += FinishWave;
    }

    private void FinishWave()
    {
        if (_currentWave % _gameStageWavesCount == 0)
        {
            MajorWaveEnded?.Invoke();
            BigStage++;
        }
        else
        {
            MinorWaveEnded?.Invoke();
        }

        _currentWave++;

        StartCoroutine(StartNextWave());
    }

    private void StartWave()
    {
        WaveStarted?.Invoke();
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(5);
        
        StartWave();
    }
}
