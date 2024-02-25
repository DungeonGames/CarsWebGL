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
    

    private void Start()
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

        int spawnersToUse = Random.Range(0, availableSpawners.Count);

        int enemiesPerSpawner = Mathf.CeilToInt((float)enemiesToSpawn / spawnersToUse);
        
        for (int i = 0; i < spawnersToUse; i++)
        {
            availableSpawners[i].SpawnPool(enemiesToSpawn,  _wavesManager.CurrentWave, _playerMover);
        }
    }

    private List<EnemySpawner> GetAllAvailableSpawners()
    {
        int currentWaveZone = (int)_wavesManager.CurrentWave;

        List<EnemySpawner> result = _spawners
            .Where(spawner => (int)spawner.Zone <= currentWaveZone)
            .ToList();

        return result;
    }
}
