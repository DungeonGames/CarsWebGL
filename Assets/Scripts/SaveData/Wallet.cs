using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _gemText;

    private const string CoinsSave = "CoinsSave";
    private const string GemsSave = "GemsSave";

    public int Coins { get; private set; }
    public int Gems { get; private set; }

    private void Start()
    {
        Load();
        ChangeCollectedText();
    }

    public void DecreaseCoins(int value)
    {
        if (TryDecreaseCoins(value))
        {
            Coins -= value;
            ChangeCollectedText();
            Save();
        }      
    }

    public bool TryDecreaseCoins(int value)
    {
        return value <= Coins;
    }

    public void DecreaseGems(int value)
    {
        if (TryDecreaseGems(value))
        {
            Gems -= value;
            ChangeCollectedText();
            Save();
        }
    }

    public bool TryDecreaseGems(int value)
    {
        return value <= Gems;
    }

    public void AddReward(int coins, int gems)
    {
        Coins += coins;
        Gems += gems;
        Save();
    }

    private void Load()
    {
        var dataCoins = SaveSystem.Load<SaveData.PlayerData>(CoinsSave);
        Coins = dataCoins.Coins;

        var dataGems = SaveSystem.Load<SaveData.PlayerData>(GemsSave);
        Gems = dataGems.Gems;
    }

    private void Save()
    {
        SaveSystem.Save(CoinsSave, GetSaveSnapshot());
        SaveSystem.Save(GemsSave, GetSaveSnapshot());
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            Coins = Coins,
            Gems = Gems,
        };

        return data;
    }

    private void ChangeCollectedText()
    {
        if (_coinText != null)
        {
            _coinText.text = Coins.ToString();
        }

        if (_gemText != null)
        {
            _gemText.text = Gems.ToString();
        }
    }
}