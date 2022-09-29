using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] private Sprite _imageUpgrade;
    [SerializeField] private int _price;
    [SerializeField] private string _localizedUpgradeName;
    [SerializeField] private string _localizedUpgradeLevel;
    [SerializeField] private LeanToken _currentLevelToken;

    protected int _currentLevel;

    public event UnityAction Buyed;
    public event UnityAction<int> CurrentLevelChanged;

    public Sprite ImageUpgrade => _imageUpgrade;
    public int Price => _price;
    public int CurrentLevel => _currentLevel;
    public string LocalizedUpgradeName => _localizedUpgradeName;
    public string LocalizedUpgradeLevel => _localizedUpgradeLevel;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        CurrentLevelChanged?.Invoke(_currentLevel);
        _currentLevelToken.SetValue(_currentLevel);
    }

    public abstract void Load();

    public abstract void Save();

    public abstract SaveData.PlayerData GetSaveSnapshot();

    public void UpdateCurrentLevel()
    {
        _currentLevelToken.SetValue(_currentLevel);
        LeanLocalization.UpdateTranslations();
    }

    public void SellUpgrade()
    {
        _currentLevel++;
        _currentLevelToken.SetValue(_currentLevel);
        LeanLocalization.UpdateTranslations();
        Buyed.Invoke();
        Save();
    }
}
