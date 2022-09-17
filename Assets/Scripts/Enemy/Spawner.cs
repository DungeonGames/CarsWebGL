using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public int Count;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private enum GizmoType { Never, SelectedOnly, Always }

    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Car _player;
    [SerializeField] private GameUiHandler _gameStartHandler;
    [SerializeField] private EnemyWalls _enemyWalls;
    [SerializeField] private float _spawnRadius = 15f;
    [SerializeField] private Color _color;
    [SerializeField] private GizmoType _showSpawnRegion;

    private List<Enemy> _pooledEnemys = new List<Enemy>();
    private int _currentWaveNumber = 0;
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

    private void Awake()
    {
        Initialize(_waves, _player);
        _spawned = GetSpawnedEnemyCount();
        _deads = 0;
    }

    private void OnEnable()
    {
        _gameStartHandler.GameStart += OnGameStarted;
    }

    private void OnDisable()
    {
        _gameStartHandler.GameStart -= OnGameStarted;

        for (int i = 0; i < _pooledEnemys.Count; i++)
        {
            _pooledEnemys[i].Die -= OnEnemyDead;
        }
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

    private void Initialize(List<Wave> waves, Car player)
    {
        for (int i = 0; i < _waves.Count; i++)
        {
            for (int j = 0; j < waves[i].Count; j++)
            {
                Vector3 position = transform.position + Random.insideUnitSphere * _spawnRadius;
                Enemy enemy = Instantiate(waves[i].Template, position, transform.rotation);
                enemy.transform.position = position;
                enemy.Die += OnEnemyDead;
                enemy.Init(player, this);
                enemy.transform.SetParent(transform);
                _pooledEnemys.Add(enemy);
            }
        }       
    }

    private int GetSpawnedEnemyCount()
    {
        int count = 0;

        for (int i = 0; i < _waves.Count; i++)
        {
            count += _waves[i].Count;
        }

        return count;
    }

    private void OnGameStarted()
    {
        _enemyWalls.gameObject.SetActive(false);
        GameStart?.Invoke();
    }   
}
