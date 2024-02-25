using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _greenEnemy;
    [SerializeField] private GameObject _blueEnemy;
    [SerializeField] private GameObject _orangeEnemy;

    [SerializeField] private EnemyWaveContentGenerator _waveGenerator;

    [SerializeField] private float _spawnRadius = 7f;

    public SpawnZone Zone = SpawnZone.FirstZone;

    public List<GameObject> SpawnPool(int spawnCount, int level, PlayerMover player)
    {
        List<GameObject> spawned = new List<GameObject>();
        var wave = _waveGenerator.GenerateWave(spawnCount);

        foreach (var enemy in wave.SpawnPool)
        {
            spawned.Add(Spawn(enemy, level, player));
        }

        return spawned;
    }

    private GameObject Spawn(EnemyToSpawn enemyToSpawn, int level, PlayerMover player)
    {
        GameObject prefab;

        switch (enemyToSpawn.Type)
        {
            case EnemyToSpawn.EnemyType.Green:
                prefab = _greenEnemy;
                break;
            case EnemyToSpawn.EnemyType.Blue:
                prefab = _blueEnemy;
                break;
            case EnemyToSpawn.EnemyType.Orange:
                prefab = _orangeEnemy;
                break;
            default:
                prefab = _greenEnemy;
                break;
        }

        GameObject enemyInstance = Instantiate(prefab, GetSpawnPos(), Quaternion.identity);

        enemyInstance.GetComponent<Enemy>().InitEnemy(level, player);

        return enemyInstance;
    }

    private Vector3 GetSpawnPos()
    {
        return transform.position + Random.insideUnitSphere * _spawnRadius;
    }

    public enum SpawnZone
    {
        FirstZone = 1,
        SecondZone = 2,
        ThirdZone = 3,
        FourthZone = 4,
        FifthZone = 5,
        SixthZone = 6,
        SeventhZone = 7,
        EightZone = 8,
        NinthZone = 9,
        TenthZone = 10,
        EleventhZone = 11,
        TwelfthZone = 12
    }
}