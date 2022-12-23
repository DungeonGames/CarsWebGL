using SaveData;
using UnityEngine;

public class MinigunUpgrade : Upgrade
{
    [SerializeField] private Gun _gun;

    private const string MinigunLevel = "MinigunLevel";

    public override PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            MinigunLevel = _currentLevel,
        };

        return data;
    }

    public override void Load()
    {
        var dataGun = SaveSystem.Load<SaveData.PlayerData>(MinigunLevel);
        _currentLevel = dataGun.MinigunLevel;
        _currentValue = _gun.CurrentFireRate;
    }

    public override void Save()
    {
        SaveSystem.Save(MinigunLevel, GetSaveSnapshot());
    }

    public override void SetValue()
    {
        _currentValue = _gun.CurrentFireRate;
        _onLevelValue = _gun.FireRateOnLevel;
    }
}
