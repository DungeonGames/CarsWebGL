using Lean.Localization;
using System;
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

    private float _multiplier = 1.1f;
    private int _startPrice = 100;

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

        if (_currentLevel > 0)
        {
            _price = _price * (int)Math.Pow(_multiplier, _currentLevel);
        }
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
        _price = (int)(_startPrice * Math.Pow(_multiplier, _currentLevel));
        Debug.Log(_price);
        _currentLevelToken.SetValue(_currentLevel);
        LeanLocalization.UpdateTranslations();
        Buyed.Invoke();
        Save();
    }
}
