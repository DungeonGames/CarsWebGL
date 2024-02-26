using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners;

    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WavesManager _wavesManager;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _enemiesToSpawn;
    
    public event Action MapClear;
    public event Action<int, int> EnemyCountChanged;
    
    private void Awake()
    {
        _wavesManager.WaveStarted += GetReadyToSpawn;
        _spawners.OrderBy(spawner => (int)spawner.Zone).ToList();
    }

    private void GetReadyToSpawn()
    { 
        _enemiesToSpawn = 10 + _wavesManager.CurrentWave * 2;

        Spawn(_enemiesToSpawn);
    }

    public void Spawn(int enemiesToSpawn)
    {
        List<EnemySpawner> availableSpawners = GetAllAvailableSpawners();
        Debug.Log(availableSpawners.Count());

        int spawnersToUse = Random.Range(1, availableSpawners.Count + 1);

        int enemiesPerSpawner = Mathf.CeilToInt((float)enemiesToSpawn / spawnersToUse);

        for (int i = 0; i < spawnersToUse; i++)
        {
            _spawnedEnemies.AddRange(availableSpawners[i]
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
        int currentWaveZone = (int)_wavesManager.BigStage; 

        List<EnemySpawner> result = _spawners
            .Where(spawner => (int)spawner.Zone <= currentWaveZone)
            .ToList();

        return result;
    }
}