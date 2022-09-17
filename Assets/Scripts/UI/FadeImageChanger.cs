using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeImageChanger : MonoBehaviour
{
    [SerializeField] private Image _lowHealtTrigger;
    [SerializeField] private Car _car;

    [SerializeField] private float _toFade = 100f;
    [SerializeField] private float _fadeSpeed = 1f;

    private void OnEnable()
    {
        _car.LowHealh += OnLowHealt;
    }

    private void OnDisable()
    {
        _car.LowHealh -= OnLowHealt;
    }

    private void OnLowHealt()
    {
        _lowHealtTrigger.gameObject.SetActive(true);
        _lowHealtTrigger.DOFade(_toFade, _fadeSpeed).SetLoops(-1, LoopType.Yoyo);
    }

}
