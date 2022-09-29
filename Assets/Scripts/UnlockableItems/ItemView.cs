using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Lean.Localization;

public class ItemView : MonoBehaviour
{
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedName;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedDescription;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Image _image;
    [SerializeField] private Image _gemImage;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _buyed;
    
    private UnlockableItem _unlockableItem;

    public UnlockableItem UnlockableItem => _unlockableItem;

    public event UnityAction<ItemView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(UnlockableItem unlockableItem)
    {
        if (unlockableItem.IsUnlock)
        {
            _sellButton.interactable = true;
        }

        _unlockableItem = unlockableItem;
        _image.sprite = unlockableItem.Sprite;
        _price.text = unlockableItem.Price.ToString();
        _localizedName.TranslationName = unlockableItem.LocalizedItemName;
        _localizedDescription.TranslationName = unlockableItem.LocalizedItemDescription;
    }

    public void DeactivateButton() => _sellButton.interactable = false;

    public void SellItem()
    {
        _gemImage.gameObject.SetActive(false);
        _price.gameObject.SetActive(false);
        _buyed.gameObject.SetActive(true);
        _unlockableItem.Buyed();
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(this);
        _unlockableItem.ActivateItem();
    }
}
