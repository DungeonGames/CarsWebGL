using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected float _damage;
    
    [SerializeField] protected Bullet BulletTemplate;
    [SerializeField] protected ParticleSystem ShootParticle;
    [SerializeField] protected CameraShake CameraShake;
    [SerializeField] private float _delayPerShot = 0.7f;
    [SerializeField] private GameUIHandler _gameStartHandler;
    [SerializeField] private Transform _body;
    [SerializeField] private ParticleSystem _upgradeEffect;

    private protected AudioResources AudioResources;

    private float _minDelayPerShot = 0.1f;
    private float _decreaseDelayPerShot = 0.01f;
    private float _timeRemaining;
    private bool _canFire = false;
    private Enemy _currentTraget;


    private void OnEnable()
    {
        if (_gameStartHandler != null)
        {
            _gameStartHandler.GameStart += StartFire;
            _gameStartHandler.GameEnd += StopFire;
        }
    }

    private void Start()
    {
        AudioResources = FindObjectOfType<AudioResources>();
        _timeRemaining = _delayPerShot;
    }

    private void Update()
    {
        if (_canFire)
            PrepareToShoot();
    }

    private void OnDisable()
    {
        if (_gameStartHandler != null)
        {
            _gameStartHandler.GameStart -= StartFire;
            _gameStartHandler.GameEnd -= StopFire;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            float newEnemyDistance = Vector3.Distance(transform.position, enemy.gameObject.transform.position);

            if (_currentTraget == null && enemy.IsAlive)
            {
                _currentTraget = enemy;
                enemy.PrepareToDie += OnDying;
            }
            else
            {
                if (_currentTraget != null || enemy.IsAlive)
                {
                    float currentEnemyDistance = Vector3.Distance(transform.position, _currentTraget.gameObject.transform.position);

                    if (newEnemyDistance < currentEnemyDistance)
                    {
                        _currentTraget.PrepareToDie -= OnDying;
                        _currentTraget = enemy;
                        enemy.PrepareToDie += OnDying;
                    }
                }               
            }          
        }
    }

    public void StartFire() => _canFire = true;

    public void StopFire() => _canFire = false;

    public abstract void Shoot(Enemy enemy);
    
    private void OnUpgradeBought()
    {
        _delayPerShot -= _decreaseDelayPerShot;

        _upgradeEffect.Play();
        if (_delayPerShot <= _minDelayPerShot)
        {
            _delayPerShot = _minDelayPerShot;
        }
    }

    private void OnCurrentLevelChanged(int level)
    {
        _delayPerShot -= _decreaseDelayPerShot * level;

        if (_delayPerShot <= _minDelayPerShot)
        {
            _delayPerShot = _minDelayPerShot;
        }
    }

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

    private void OnDying(Enemy enemy)
    {
        enemy.PrepareToDie -= OnDying;
        _currentTraget = null;
    }

    public void UpgradeDamage(float bonusPercent)
    {
        _damage *= bonusPercent;
    }

    public void UpgradeFireRate(float bonusPercent)
    {
        _delayPerShot /= bonusPercent;
    }
}