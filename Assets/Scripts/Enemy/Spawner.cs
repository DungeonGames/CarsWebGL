using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private enum GizmoType { Never, SelectedOnly, Always }

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Car _player;
    [SerializeField] private GameUiHandler _gameStartHandler;
    [SerializeField] private EnemyWalls _enemyWalls;
    [SerializeField] private float _spawnRadius = 15f;
    [SerializeField] private Color _color;
    [SerializeField] private GizmoType _showSpawnRegion;

    private Wave _wave;
    private List<Enemy> _pooledEnemys = new List<Enemy>();
    private int _spawned;
    private int _deads;

    public event UnityAction<int, int> EnemyCountChanged;
    public event UnityAction GameStart;

    private void OnDrawGizmos()
    {
        if (_showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_showSpawnRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    private void OnEnable()
    {
        _levelGenerator.WaveChange += OnWaveChanged;
        _gameStartHandler.GameStart += OnGameStarted;
    }

    private void OnDisable()
    {
        _levelGenerator.WaveChange -= OnWaveChanged;
        _gameStartHandler.GameStart -= OnGameStarted;

        for (int i = 0; i < _pooledEnemys.Count; i++)
        {
            _pooledEnemys[i].Die -= OnEnemyDead;
        }
    }

    private void Start()
    {
        Initialize(_wave, _player);
        _spawned = _pooledEnemys.Count;
        _deads = 0;
    }

    private void OnWaveChanged(Wave wave) => _wave = wave;

    private void OnGameStarted()
    {
        _enemyWalls.gameObject.SetActive(false);
        GameStart?.Invoke();
    }

    public void OnEnemyDead(Enemy enemy)
    {
        _deads++;
        EnemyCountChanged?.Invoke(_deads, _spawned);
    }

    private void DrawGizmos()
    {
        Gizmos.color = new Color(_color.r, _color.g, _color.b, 0.3f);
        Gizmos.DrawSphere(transform.position, _spawnRadius);
    }

    private void Initialize(Wave wave, Car player)
    {
        if (wave.GreenCount > 0)
        {
            for (int i = 0; i < wave.GreenCount; i++)
            {
                SpawnEnemy(wave.GreenEnemyTemplate, player);
            }
        }

        if (wave.BlueCount > 0)
        {
            for (int i = 0; i < wave.BlueCount; i++)
            {
                SpawnEnemy(wave.BlueEnemyTemplate, player);
            }
        }

        if (wave.OrangeCount > 0)
        {
            for (int i = 0; i < wave.OrangeCount; i++)
            {
                SpawnEnemy(wave.OrangeEnemyTemplate, player);
            }
        }
    }

    private void SpawnEnemy(Enemy template, Car player)
    {
        Vector3 position = transform.position + Random.insideUnitSphere * _spawnRadius;
        Enemy enemy = Instantiate(template, position, transform.rotation);
        enemy.transform.position = position;
        enemy.Die += OnEnemyDead;
        enemy.Init(player, this);
        enemy.transform.SetParent(transform);
        _pooledEnemys.Add(enemy);
    }
}
