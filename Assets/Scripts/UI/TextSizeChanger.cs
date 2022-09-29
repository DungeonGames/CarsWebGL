using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextSizeChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private float _scaleTo = 1.1f;
    private float _scaleSpeed = 1f;

    private void Start()
    {
        ChangeSizeText();
    }

    private void ChangeSizeText()
    {
        _text.gameObject.transform.DOScale(_scaleTo, _scaleSpeed).SetLoops(-1, LoopType.Yoyo);
    }
}
