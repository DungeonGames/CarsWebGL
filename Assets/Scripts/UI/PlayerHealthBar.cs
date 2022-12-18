using DG.Tweening;
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
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _slider.value = 1;
        _playerBag.CarChanged += OnCarChanged;
    }

    private void LateUpdate()
    {
        _slider.transform.LookAt(new Vector3(_slider.transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        _slider.transform.Rotate(0, 180, 0);
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
