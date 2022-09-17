using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMaterialSeter))]
[RequireComponent(typeof(Boid))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private int _health;
    [SerializeField] private float _maxScale = 2.5f;
    [SerializeField] private float _minScale = 1.5f;
    [SerializeField] private ParticleSystem _dieEffect;

    private int _currentHealth;
    private float _delayToDie = 1f;
    private Car _target;
    private EnemyMaterialSeter _materialSeter;
    private Boid _boid;
    private Spawner _spawner;
    private bool _isAlive = true;

    public bool IsAlive => _isAlive;

    public event UnityAction Hit;
    public event UnityAction PrepareToDie;
    public event UnityAction<Enemy> Die;

    private void Awake()
    {
        _materialSeter = GetComponent<EnemyMaterialSeter>();
        _boid = GetComponent<Boid>();
    }

    private void Start()
    {
        _currentHealth = _health;
    }

    private void OnEnable()
    {
        _materialSeter.SwitchEnded += OnMaterialSwitchEnded;
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
            car.TakeDamage(_damage);
        }
    }

    public void Init(Car target, Spawner spawner)
    {
        _boid.Initialize(null);
        float randomScale = Random.Range(_minScale, _maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        _spawner = spawner;
        _target = target;
        _spawner.GameStart += OnGameStarted;
    }

    public void TakeDamage(int value)
    {
        if (CanDecreaseHealth(value))
        {
            _currentHealth -= value;
            Hit?.Invoke();
        }
        else
        {
            _isAlive = false;
            PrepareToDie?.Invoke();
            _boid.enabled = false;
        }
    }

    public void CathHoleTrap()
    {
        _isAlive = false;
        gameObject.SetActive(false);
        _boid.enabled = false;
        Die?.Invoke(this);
    }

    private bool CanDecreaseHealth(float value)
    {
        return _currentHealth - value > 0;
    }

    private void OnGameStarted() => _boid.SetTarget(_target.transform);

    private void OnMaterialSwitchEnded()
    {
        StartCoroutine(DelayToDie(_delayToDie));
    }

    private IEnumerator DelayToDie(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
        Die?.Invoke(this);
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
    }
}
