using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerBag _playerBag;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _lowHealtTrigger;

    private Car _car;
    private const float ToFade = 0.5f;
    private const float FadeSpeed = 1f;

    private void OnEnable()
    {
        _slider.value = 1;
        _playerBag.CarChanged += OnCarChanged;
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _car.LowHealh -= OnLowHealt;
    }

    private void OnCarChanged(Car car)
    {
        if (_car != null)
        {
            _car.HealtChange -= OnValueChanged;
            _car.LowHealh -= OnLowHealt;
        }

        _car = car;
        _car.LowHealh += OnLowHealt;
        _car.HealtChange += OnValueChanged;
    }

    private void OnLowHealt()
    {
        _lowHealtTrigger.gameObject.SetActive(true);
        _lowHealtTrigger.DOFade(ToFade, FadeSpeed).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnValueChanged(float value, float maxValue)
    {
        _slider.value = value / maxValue;      
    }
}
