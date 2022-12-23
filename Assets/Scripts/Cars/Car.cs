using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private ParticleSystem _upgradeEffect;
    [SerializeField] private ParticleSystem _hitEffect;

    private float _currentHealth;
    private float _lowHealthValueTrigger;
    private float _increaseMaxHealth = 5f;

    public Upgrade CarUpgrade => _upgrade;
    public float MaxSpeed => _maxSpeed;
    public float MaxHealth => _health;
    public float IncreasesValue => _increaseMaxHealth;

    public event UnityAction LowHealh;
    public event UnityAction Died;
    public event UnityAction<float, float> HealtChange;

    private void OnEnable()
    {
        _upgrade.Buyed += OnUpgradeBuyed;
        _upgrade.CurrentLevelChanged += OnCurrentLevelChanged;
    }

    private void Start()
    {
        UpdateHealt();
    }

    private void OnDisable()
    {
        _upgrade.Buyed -= OnUpgradeBuyed;
        _upgrade.CurrentLevelChanged -= OnCurrentLevelChanged;
    }

    public void TakeDamage(float value)
    {
        
        if (CanDecreaseHealth(value))
        {
            _currentHealth -= value;
            HealtChange?.Invoke(_currentHealth, _health);
            _hitEffect.Play();

            if (_currentHealth < _lowHealthValueTrigger)
                LowHealh?.Invoke();
        }
        else
        {
            Died?.Invoke();
        }
    }

    private void OnUpgradeBuyed()
    {
        _health += _increaseMaxHealth;
        _upgradeEffect.Play();
        UpdateHealt();
    }

    private void OnCurrentLevelChanged(int level)
    {
        _health += _increaseMaxHealth * level;
        UpdateHealt();
    }

    private void UpdateHealt()
    {
        _currentHealth = _health;
        _lowHealthValueTrigger = _health / 3;
    }

    private bool CanDecreaseHealth(float value)
    {
        return _currentHealth - value > 0;
    }
}
