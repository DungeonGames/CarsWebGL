using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private EnemySpawnerController _spawner;

    public event UnityAction LevelComplete;

    private void OnEnable()
    {
        _slider.value = 0;
        _spawner.EnemyCountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemyCountChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value, int maxValue)
    {
        _slider.value = (float)(maxValue - value) / maxValue;
        Debug.Log(maxValue + " " + value);

        if(_slider.value == _slider.maxValue)
        {
            LevelComplete?.Invoke();
        }
    }
}
