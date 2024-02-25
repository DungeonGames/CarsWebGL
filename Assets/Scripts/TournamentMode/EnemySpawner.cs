using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _greenEnemy;
    [SerializeField] private GameObject _blueEnemy;
    [SerializeField] private GameObject _orangeEnemy;

    [SerializeField] private EnemyWaveContentGenerator _waveGenerator;

    public SpawnZone Zone = SpawnZone.FirstZone;

    public void SpawnPool(int spawnCount, int level, PlayerMover player)
    {
        var wave = _waveGenerator.GenerateWave(spawnCount);

        foreach (var enemy in wave.SpawnPool)
        {
            Spawn(enemy, level, player);
        }
    }
    
    private void Spawn(EnemyToSpawn enemyToSpawn, int level, PlayerMover player)
    {
        GameObject prefab = new GameObject();

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
        }

        GameObject enemyInstance = Instantiate(prefab, transform.position, Quaternion.identity);
        
        enemyInstance.GetComponent<Enemy>().InitEnemy(level, player);
    }

    public enum SpawnZone
    {
        FirstZone = 1,
        SecondZone = 2,
        ThirdZone = 3,
        FourthZone = 4,
        FifthZone =  5,
        SixthZone = 6,
        SeventhZone = 7,
        EightZone = 8,
        NinthZone = 9,
        TenthZone = 10,
        EleventhZone = 11,
        TwelfthZone = 12
    }
}
