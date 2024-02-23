using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(EnemyMaterialSeter))]
[RequireComponent(typeof(Boid))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _statsScaleCoef = 1; // defines how fast stats scale depends on level
    [SerializeField] private float _statsSpread = 1.5f;
    
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private float _maxScale = 2.5f;
    [SerializeField] private float _minScale = 1.5f;
    [SerializeField] private ParticleSystem _dieEffect;

    private int _level = 1; // current wave in fact
    private float _lowerBoundStatsCoef;
    private float _upperBoundStatsCoef;

    private float _currentHealth;
    private float _delayToDie = 1f;
    private float _forceConntact = 30f;
    private PlayerMover _target;
    private EnemyMaterialSeter _materialSeter;
    private AudioResources _audioResources;
    private Rigidbody _rigidbody;
    private Boid _boid;
    private Spawner _spawner;
    private bool _isAlive = true;

    private const string EnemyDied = "EnemyDied";

    public bool IsAlive => _isAlive;

    public event UnityAction Hit;
    public event UnityAction<Enemy> PrepareToDie;
    public event UnityAction<Enemy> Die;

    private void Awake()
    {
        _materialSeter = GetComponent<EnemyMaterialSeter>();
        _boid = GetComponent<Boid>();
        InitEnemy(1); // should be removed
    }

    private void OnEnable()
    {
        _materialSeter.SwitchEnded += OnMaterialSwitchEnded;
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Should be called at instantiation. Sets enemy health and damage
    /// </summary>
    /// <param name="level"></param>
    public void InitEnemy(int level)
    {
        _level = level;
        _lowerBoundStatsCoef = _statsScaleCoef * Mathf.Sqrt(_level);
        _upperBoundStatsCoef = _statsScaleCoef * Mathf.Sqrt(_statsSpread * _level);
        _currentHealth = GetRealHealth();
    }

    private void Start()
    {
        _audioResources = FindObjectOfType<AudioResources>();
    }

    private void OnDisable()
    {
        _materialSeter.SwitchEnded -= OnMaterialSwitchEnded;
        _spawner.GameStart -= OnGameStarted;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Car car))
        {
            Discard();
            car.TakeDamage(GetRealDamage());
        }
    }

    private float GetRealDamage()
    {
        float damage = Random.Range(_damage * _lowerBoundStatsCoef, _damage * _upperBoundStatsCoef);

        return damage;
    }

    private float GetRealHealth()
    {
        float health = Random.Range(_health * _lowerBoundStatsCoef, _health * _upperBoundStatsCoef);

        return health;
    }

    public void Init(PlayerMover target, Spawner spawner)
    {
        _boid.Initialize(null);
        float randomScale = Random.Range(_minScale, _maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        _spawner = spawner;
        _target = target;
        _spawner.GameStart += OnGameStarted;
    }

    public void Discard()
    {
        _rigidbody.AddForce(-transform.forward * _forceConntact, ForceMode.Impulse);
    }

    public void TakeDamage(float value)
    {
        if (CanDecreaseHealth(value))
        {
            _currentHealth -= value;
            Hit?.Invoke();
        }
        else
        {
            _isAlive = false;
            PrepareToDie?.Invoke(this);
            _boid.enabled = false;
        }
    }

    public void CathHoleTrap()
    {
        _isAlive = false;
        gameObject.SetActive(false);
        _boid.enabled = false;
        PrepareToDie?.Invoke(this);
        Die?.Invoke(this);
        _audioResources.PlaySound(EnemyDied);
    }

    private void OnMaterialSwitchEnded()
    {
        StartCoroutine(DelayToDie(_delayToDie));
    }

    private void OnGameStarted() => _boid.SetTarget(_target.transform);

    private bool CanDecreaseHealth(float value)
    {
        return _currentHealth - value > 0;
    }

    private IEnumerator DelayToDie(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
        Die?.Invoke(this);
        _audioResources.PlaySound(EnemyDied);
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
    }
}