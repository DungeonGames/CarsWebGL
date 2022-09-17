using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Bullet BulletTemplate;
    [SerializeField] protected ParticleSystem ShootParticle;

    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private GameUiHandler _gameStartHandler;
    [SerializeField] private Transform _body;

    private float _delayPerShot = 0.7f;
    private float _decreaseDelayPerShot = 0.1f;
    private float _timeRemaining;
    private bool _inGame = false;
    private Enemy _currentTraget;

    public Upgrade GunUpgrade => _upgrade;

    private void Start()
    {
        _timeRemaining = _delayPerShot;
    }

    private void Update()
    {
        if (_inGame)
            PrepareToShoot();
    }

    private void OnEnable()
    {
        _gameStartHandler.GameStart += OnGameStarted;
        _gameStartHandler.GameEnd += OnGameEnded;
        _upgrade.Buyed += OnUpgradeBuyed;
    }

    private void OnDisable()
    {
        _gameStartHandler.GameStart -= OnGameStarted;
        _gameStartHandler.GameEnd -= OnGameEnded;
        _upgrade.Buyed -= OnUpgradeBuyed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_currentTraget == null && enemy.IsAlive)
            {
                _currentTraget = enemy;
                enemy.Die += OnDying;
            }
        }
    }

    public abstract void Shoot(Enemy enemy);

    private void OnUpgradeBuyed() => _delayPerShot -= _decreaseDelayPerShot;

    private void PrepareToShoot()
    {
        if (_currentTraget != null)
        {
            _body.LookAt(_currentTraget.transform.position);

            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = _delayPerShot;
                Shoot(_currentTraget);
            }
        }
    }

    private void OnGameEnded() => _inGame = false;

    private void OnGameStarted() => _inGame = true;

    private void OnDying(Enemy enemy)
    {
        enemy.Die -= OnDying;
        _currentTraget = null;
    }
}
