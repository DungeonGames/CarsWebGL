using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxSpeed;

    private float _currentHealth;
    private float _lowHealthValueTrigger;
    
    public float MaxSpeed => _maxSpeed;
    public event UnityAction LowHealh;
    public event UnityAction Died;
    public event UnityAction<float, float> HealtChange;

    private void Start()
    {
        UpdateHealth();
    }

    public void TakeDamage(float value)
    {
        if (CanDecreaseHealth(value))
        {
            _currentHealth -= value;
            HealtChange?.Invoke(_currentHealth, _health);

            if (_currentHealth < _lowHealthValueTrigger)
                LowHealh?.Invoke();
        }
        else
        {
            Died?.Invoke();
        }
    }

    private void UpdateHealth()
    {
        _currentHealth = _health;
        _lowHealthValueTrigger = _health / 5;
    }

    private bool CanDecreaseHealth(float value)
    {
        return _currentHealth - value > 0;
    }
}
