using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeImageChanger : MonoBehaviour
{
    [SerializeField] private Image _lowHealtTrigger;
    [SerializeField] private PlayerBag _playerBag;

    private const float ToFade = 0.5f;
    private const float FadeSpeed = 1f;

    private Car _car;

    private void OnEnable()
    {
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
            _car.LowHealh -= OnLowHealt;
        }

        _car = car;
        _car.LowHealh += OnLowHealt;
    }
    private void OnLowHealt()
    {
        _lowHealtTrigger.gameObject.SetActive(true);
        _lowHealtTrigger.DOFade(ToFade, FadeSpeed).SetLoops(-1, LoopType.Yoyo);
    }

}
