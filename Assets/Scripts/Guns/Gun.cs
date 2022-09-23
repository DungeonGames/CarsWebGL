using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Bullet BulletTemplate;
    [SerializeField] protected ParticleSystem ShootParticle;

    [SerializeField] private float _delayPerShot = 0.7f;
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private GameUiHandler _gameStartHandler;
    [SerializeField] private Transform _body;

    private float _decreaseDelayPerShot = 0.01f;
    private float _timeRemaining;
    private bool _canFire = false;
    private Enemy _currentTraget;

    public Upgrade GunUpgrade => _upgrade;

    private void Start()
    {
        _timeRemaining = _delayPerShot;
    }

    private void Update()
    {
        if (_canFire)
            PrepareToShoot();
    }

    private void OnEnable()
    {
        if (_gameStartHandler != null)
        {
            _gameStartHandler.GameStart += StartFire;
            _gameStartHandler.GameEnd += StopFire;
        }

        if (_upgrade != null)
        {
            _upgrade.Buyed += OnUpgradeBuyed;
            _upgrade.CurrentLevelChanged += OnCurrentLevelChanged;
        }
    }

    private void OnDisable()
    {
        if (_gameStartHandler != null)
        {
            _gameStartHandler.GameStart -= StartFire;
            _gameStartHandler.GameEnd -= StopFire;
        }

        if (_upgrade != null)
        {
            _upgrade.Buyed += OnUpgradeBuyed;
            _upgrade.CurrentLevelChanged -= OnCurrentLevelChanged;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_currentTraget == null && enemy.IsAlive)
            {
                _currentTraget = enemy;
                enemy.PrepareToDie += OnDying;
            }
        }
    }

    public abstract void Shoot(Enemy enemy);

    public void StopFire() => _canFire = false;

    public void StartFire() => _canFire = true;

    private void OnUpgradeBuyed() => _delayPerShot -= _decreaseDelayPerShot;

    private void OnCurrentLevelChanged(int level) => _delayPerShot -= _decreaseDelayPerShot * level;

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
}
