using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedName;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedLevel;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _sellButton;

    private Upgrade _upgrade;

    public Upgrade Upgrade => _upgrade;

    public event UnityAction<UpgradeView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        Render(_upgrade);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Upgrade upgrade)
    {
        _upgrade = upgrade;
        _icon.sprite = upgrade.ImageUpgrade;
        _price.text = upgrade.Price.ToString();
        _localizedName.TranslationName = upgrade.LocalizedUpgradeName;
        _localizedLevel.TranslationName = upgrade.LocalizedUpgradeLevel;
        upgrade.UpdateCurrentLevel();
    }

    public void DeactivateButton() => _sellButton.interactable = false;

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(this);
        Render(_upgrade);
    }
}
