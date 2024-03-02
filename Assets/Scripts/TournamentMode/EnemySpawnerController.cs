using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _startSpawners;

    [SerializeField] private List<EnemySpawner> _spawners;

    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WavesManager _wavesManager;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _enemiesToSpawn;
    private int _lowerSpawnersBound = 1;

    private List<EnemySpawner> _spawnersToUse = new List<EnemySpawner>();

    public event Action MapClear;
    public event Action<int, int> EnemyCountChanged;

    private void Awake()
    {
        _wavesManager.MinorWaveEnded += PrecomputeSpawners;
        _wavesManager.MajorWaveEnded += PrecomputeSpawners;
        _wavesManager.WaveStarted += GetReadyToSpawn;
        _spawners.OrderBy(spawner => (int)spawner.Zone).ToList();

        _spawnersToUse = _startSpawners;
    }

    private void GetReadyToSpawn()
    {
        _enemiesToSpawn = 10 + _wavesManager.CurrentWave * 2;

        Spawn(_enemiesToSpawn);
    }

    private void PrecomputeSpawners()
    {
        List<EnemySpawner> availableSpawners = GetAllAvailableSpawners();
        _lowerSpawnersBound = (int)Mathf.Sqrt(_wavesManager.CurrentWave);
        _spawnersToUse = new List<EnemySpawner>();

        _spawnersToUse = availableSpawners.OrderBy(x => Random.Range(0, Int32.MaxValue))
            .Take(Random.Range(_lowerSpawnersBound, Mathf.CeilToInt((float)availableSpawners.Count / 2))).ToList();

        foreach (var spawner in _spawnersToUse)
        {
            spawner.Highlight();
        }
    }

    private void Spawn(int enemiesToSpawn)
    {
        int enemiesPerSpawner = Mathf.CeilToInt((float)enemiesToSpawn / _spawnersToUse.Count);

        for (int i = 0; i < _spawnersToUse.Count; i++)
        {
            _spawnedEnemies.AddRange(_spawnersToUse[i]
                .SpawnPool(enemiesPerSpawner, _wavesManager.CurrentWave, _playerMover));
        }

        FindObjectOfType<BoidManager>().Init(_spawnedEnemies);
        _enemiesToSpawn = _spawnedEnemies.Count;
    }

    public void RemoveFromPool(GameObject toRemove)
    {
        _spawnedEnemies.Remove(toRemove);
        FindObjectOfType<BoidManager>().DeleteBoid(toRemove);

        EnemyCountChanged?.Invoke(_spawnedEnemies.Count, _enemiesToSpawn);

        if (_spawnedEnemies.Count == 0)
        {
            MapClear?.Invoke();
        }
    }

    private List<EnemySpawner> GetAllAvailableSpawners()
    {
        int currentWaveZone = _wavesManager.BigStage;

        List<EnemySpawner> result = _spawners
            .Where(spawner => (int)spawner.Zone <= currentWaveZone)
            .ToList();

        return result;
    }
}