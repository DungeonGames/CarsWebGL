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

    public event Action MapClear;
    
    private void Awake()
    {
        _wavesManager.WaveStarted += GetReadyToSpawn;
        _spawners.OrderBy(spawner => (int)spawner.Zone).ToList();
    }

    private void GetReadyToSpawn()
    {
        int enemiesToSpawn = 10 + _wavesManager.CurrentWave * 2;

        Spawn(enemiesToSpawn);
    }

    public void Spawn(int enemiesToSpawn)
    {
        List<EnemySpawner> availableSpawners = GetAllAvailableSpawners();

        int spawnersToUse = Random.Range(1, availableSpawners.Count + 1);

        int enemiesPerSpawner = Mathf.CeilToInt((float)enemiesToSpawn / spawnersToUse);

        for (int i = 0; i < spawnersToUse; i++)
        {
            _spawnedEnemies.AddRange(availableSpawners[i]
                .SpawnPool(enemiesToSpawn, _wavesManager.CurrentWave, _playerMover));
        }
        
        FindObjectOfType<BoidManager>().Init(_spawnedEnemies);
    }
    
    public void RemoveFromPool(GameObject toRemove)
    {
        _spawnedEnemies.Remove(toRemove);
        FindObjectOfType<BoidManager>().DeleteBoid(toRemove);

        if (_spawnedEnemies.Count == 0)
        {
            MapClear?.Invoke();
        }
    }

    private List<EnemySpawner> GetAllAvailableSpawners()
    {
        int currentWaveZone = 10; //(int)_wavesManager.BigStage; 

        List<EnemySpawner> result = _spawners
            .Where(spawner => (int)spawner.Zone <= currentWaveZone)
            .ToList();

        return result;
    }
}