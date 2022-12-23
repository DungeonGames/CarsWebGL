using SaveData;
using UnityEngine;

public class SubaruUpgrade : Upgrade
{
    [SerializeField] private Car _car;

    private const string SubaruLevel = "SubaruLevel";

    public override PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            SubaruLevel = _currentLevel,
        };

        return data;
    }

    public override void Load()
    {
        var dataCar = SaveSystem.Load<SaveData.PlayerData>(SubaruLevel);
        _currentLevel = dataCar.SubaruLevel;
    }

    public override void Save()
    {
        SaveSystem.Save(SubaruLevel, GetSaveSnapshot());
    }

    public override void SetValue()
    {
        _currentValue = _car.MaxHealth;
        _onLevelValue = _car.IncreasesValue;
    }
}
