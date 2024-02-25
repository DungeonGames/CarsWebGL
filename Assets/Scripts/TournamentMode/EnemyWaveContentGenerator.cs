using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveContentGenerator : MonoBehaviour
{
    [SerializeField] private WavesManager _wavesManager;

    private int _blueSpawnChance = 1;
    private int _orangeSpawnChance = 0;

    private float _blueGrowthCoef = 0.4f;
    private float _orangeGrowthCoef = 0.2f;
    
    private int _enemiesAmount = 20;

    public int EnemiesToSpawn => _enemiesAmount;

    private void Start()
    {
        EnemyWave wave = GenerateWave();
        Debug.Log(wave.SpawnPool.Count);
    }


    public void MinorWaveFinished()
    {
        _blueSpawnChance++;
        _orangeSpawnChance++;
    }

    public void MajorWaveFinished()
    {
        _blueSpawnChance += (int)(_wavesManager.CurrentWave * _blueGrowthCoef);
        _orangeSpawnChance += (int)(_wavesManager.CurrentWave * _orangeGrowthCoef);
    }

    public EnemyWave GenerateWave(int spawnCount = -1)
    {
        if (spawnCount == -1)
        {
            spawnCount = EnemiesToSpawn;
        }
        
        List<EnemyToSpawn> pool = new List<EnemyToSpawn>();

        while(pool.Count <= spawnCount)
        {
            if (_orangeSpawnChance >= GetRandomNumber())
            {
                pool.Add(new EnemyToSpawn(EnemyToSpawn.EnemyType.Orange));
            }
            
            if (_blueSpawnChance >= GetRandomNumber())
            {
                pool.Add(new EnemyToSpawn(EnemyToSpawn.EnemyType.Blue));
            }
            
            pool.Add(new EnemyToSpawn(EnemyToSpawn.EnemyType.Green));
        }

        return new EnemyWave(pool);
    }

    private int GetRandomNumber()
    {
        return Random.Range(0, 200);
    }
    
}

public class EnemyWave
{
    public List<EnemyToSpawn> SpawnPool;

    public EnemyWave(List<EnemyToSpawn> spawnPool)
    {
        SpawnPool = spawnPool;
    }
}

public class EnemyToSpawn
{
    public EnemyType Type;

    public EnemyToSpawn(EnemyType type)
    {
        Type = type;
    }
    
    public enum EnemyType
    {
        Green,
        Blue,
        Orange
    }
}
