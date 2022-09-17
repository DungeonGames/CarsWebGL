using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Upgrade _upgrade;

    private float _currentHealth;
    private float _lowHealthValueTrigger;
    private float _increaseMaxHealth = 50f;

    public Upgrade CarUpgrade => _upgrade;

    public event UnityAction LowHealh;
    public event UnityAction Died;


    private void Start()
    {
        UpdateHealt();
    }

    private void OnEnable()
    {
        _upgrade.Buyed += OnUpgradeBuyed;
    }

    private void OnDisable()
    {
        _upgrade.Buyed -= OnUpgradeBuyed;
    }

    public void TakeDamage(float value)
    {
        if (CanDecreaseHealth(value))
        {
            _currentHealth -= value;

            if (_currentHealth < _lowHealthValueTrigger)
                LowHealh?.Invoke();
        }
        else
        {
            Died?.Invoke();
        }
    }

    private bool CanDecreaseHealth(float value)
    {
        return _currentHealth - value > 0;
    }

    private void UpdateHealt()
    {
        _currentHealth = _health;
        _lowHealthValueTrigger = _health / 3;
    }

    private void OnUpgradeBuyed()
    {
        _health += _increaseMaxHealth;
        UpdateHealt();
    }
}
