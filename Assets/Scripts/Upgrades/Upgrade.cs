using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] private Sprite _imageUpgrade;
    [SerializeField] private int _price;
    [SerializeField] private string _localizedUpgradeName;
    [SerializeField] private string _localizedCurrentValue;
    [SerializeField] private string _localizedOnLevelValue;
    [SerializeField] private LeanToken _currentLevelToken;
    [SerializeField] private LeanToken _currentValueToken;
    [SerializeField] private LeanToken _onLevelValueToken;

    protected int _currentLevel;
    protected float _currentValue;
    protected float _onLevelValue;

    private float _multiplier = 1.1f;
    private int _startPrice = 100;

    public event UnityAction Buyed;
    public event UnityAction<int> CurrentLevelChanged;

    public Sprite ImageUpgrade => _imageUpgrade;
    public int Price => _price;
    public int CurrentLevel => _currentLevel;
    public string LocalizedUpgradeName => _localizedUpgradeName;
    public string LocalizedCurrentValue => _localizedCurrentValue;
    public string LocalizedOnLevelValue => _localizedOnLevelValue;

    private void Awake()
    {
        Load();        
    }

    private void Start()
    {      
        CurrentLevelChanged?.Invoke(_currentLevel);
        _currentLevelToken.SetValue(_currentLevel);
        _currentValueToken.SetValue(_currentValue);
        _onLevelValueToken.SetValue(_onLevelValue);
        SetValue();
    }

    private void OnEnable()
    {
        if (_currentLevel > 0)
        {
            _price = (int)(_startPrice * Math.Pow(_multiplier, _currentLevel));
        }
    }

    public abstract void Load();

    public abstract void Save();
    public abstract void SetValue();

    public abstract SaveData.PlayerData GetSaveSnapshot();

    public void UpdateCurrentLevel()
    {
        _currentLevelToken.SetValue(_currentLevel);
        _currentValueToken.SetValue(_currentValue);
        _onLevelValueToken.SetValue(_onLevelValue);
        LeanLocalization.UpdateTranslations();  
    }

    public void SellUpgrade()
    {
        _currentLevel++;
        _price = (int)(_startPrice * Math.Pow(_multiplier, _currentLevel));
        _currentLevelToken.SetValue(_currentLevel);
        _currentValueToken.SetValue(_currentValue);
        _onLevelValueToken.SetValue(_onLevelValue);
        LeanLocalization.UpdateTranslations();
        Buyed.Invoke();
        Save();
        SetValue();
    }
}
