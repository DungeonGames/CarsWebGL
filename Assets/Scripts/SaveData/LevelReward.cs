using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(GameUiHandler))]
public class LevelReward : MonoBehaviour
{
    [SerializeField] private int _coinsReward;
    [SerializeField] private int _gemsReward;
    [SerializeField] private List<UnlockableItem> _unlockableItems;

    private const string UnlokableItems = "UnlokableItems";
    private const string CurrentQuanity = "CurrentQuanity";
    private bool[] _isUnlockableFlags;
    private int _currentQuanity;
    private int _coinMultiplier = 2;
    private Wallet _wallet;
    private GameUiHandler _gameHandler;
    private UnlockableItem _currentItem;

    public int CoinsReward => _coinsReward;
    public int GemsReward => _gemsReward;

    public event UnityAction<UnlockableItem, int> CurrentItem;
    public event UnityAction CoinRewardChange;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _gameHandler = GetComponent<GameUiHandler>();

        _isUnlockableFlags = new bool[_unlockableItems.Count];

        Load();

        for (int i = 0; i < _unlockableItems.Count; i++)
        {
            if (_isUnlockableFlags[i] == true)
            {
                _unlockableItems[i].Unlocked();
                CurrentItem?.Invoke(null, _currentQuanity);
            }
            else
            {
                _currentItem = _unlockableItems[i];
                CurrentItem?.Invoke(_unlockableItems[i], _currentQuanity);
                break;
            }
        }
    }

    private void OnEnable()
    {
        _gameHandler.GameEnd += OnLevelEnded;
    }

    private void OnDisable()
    {
        _gameHandler.GameEnd -= OnLevelEnded;
    }

    public void InceaseCoinReward()
    {
        _coinsReward *= _coinMultiplier;
        CoinRewardChange?.Invoke();
    }

    public void OnClaimRewardButtonClick()
    {
        if (_currentItem != null && _currentQuanity >= _currentItem.Quanity)
        {
            _currentQuanity = 0;
            _currentItem.Unlocked();
        }
        else
        {
            _currentQuanity++;
        }

        Save();
        _wallet.AddReward(_coinsReward, _gemsReward);
    }

    private void Load()
    {
        var dataItems = SaveSystem.Load<SaveData.PlayerData>(UnlokableItems);
        var dataQuanity = SaveSystem.Load<SaveData.PlayerData>(CurrentQuanity);

        _currentQuanity = dataQuanity.CurrentUnlockableQuanity;
        _isUnlockableFlags = dataItems.IsUnlockableItemFlags;
    }

    private void OnLevelEnded()
    {
        if (_currentItem != null)
        {
            CurrentItem?.Invoke(_currentItem, _currentQuanity);
        }
    }

    private void Save()
    {
        SaveSystem.Save(UnlokableItems, GetSaveSnapshot());
        SaveSystem.Save(CurrentQuanity, GetSaveSnapshot());
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        for (int i = 0; i < _unlockableItems.Count; i++)
        {
            _isUnlockableFlags[i] = _unlockableItems[i].IsUnlock;
        }

        var data = new SaveData.PlayerData()
        {
            IsUnlockableItemFlags = _isUnlockableFlags,
            CurrentUnlockableQuanity = _currentQuanity,
        };

        return data;
    }
}
