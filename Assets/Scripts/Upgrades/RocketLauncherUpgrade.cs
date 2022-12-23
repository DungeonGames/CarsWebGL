using SaveData;
using UnityEngine;

public class RocketLauncherUpgrade : Upgrade
{
    [SerializeField] private Gun _gun;

    private const string RocketLauncherLevel = "LauncherLevel";

    public override PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            RocketLauncherLevel = _currentLevel,
        };

        return data;
    }

    public override void Load()
    {
        var dataGun = SaveSystem.Load<SaveData.PlayerData>(RocketLauncherLevel);
        _currentLevel = dataGun.RocketLauncherLevel;
    }

    public override void Save()
    {
        SaveSystem.Save(RocketLauncherLevel, GetSaveSnapshot());
    }

    public override void SetValue()
    {
        _currentValue = _gun.CurrentFireRate;
        _onLevelValue = _gun.FireRateOnLevel;
    }
}
