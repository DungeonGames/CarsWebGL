using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class UnlockableItem : MonoBehaviour
{
    [SerializeField] private protected PlayerBag PlayerBag;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private int _quanity;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private string _localizedItemName;
    [SerializeField] private string _localizedItemDescription;
    [SerializeField] private LeanToken _currentQuanityToken;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _leanGUI;
    [SerializeField] private TMP_Text _unlockedText;
    [SerializeField] private TMP_Text _quanityText;

    private protected bool IsBuyed = false;

    private bool _isUnlock = false;

    public int Quanity => _quanity;
    public Sprite Sprite => _sprite;
    public int Price => _price;
    public bool IsBuyedItem => IsBuyed;
    public bool IsUnlock => _isUnlock;
    public string LocalizedItemName => _localizedItemName;
    public string LocalizedItemDescription => _localizedItemDescription;

    public event UnityAction<UnlockableItem> Unlock;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public abstract void Load();

    public abstract void Save();

    public abstract void ActivateItem();

    public abstract SaveData.PlayerData GetSaveSnapshot();

    public void ShowProgress(int currentQuanity)
    {
        _progressBar.value = (float)currentQuanity / _quanity;
        _currentQuanityToken.SetValue(_quanity - currentQuanity);
        _leanGUI.UpdateLocalization();

        if (_quanity - currentQuanity == 0)
        {
            _quanityText.gameObject.SetActive(false);
            _unlockedText.gameObject.SetActive(true);
        }
        else
        {
            _quanityText.gameObject.SetActive(true);
            _unlockedText.gameObject.SetActive(false);
        }
    }

    public void Unlocked()
    {
        _isUnlock = true;
        Unlock?.Invoke(this);
        Save();
    }

    public void Buyed()
    {
        IsBuyed = true;
        Save();
    }
}
