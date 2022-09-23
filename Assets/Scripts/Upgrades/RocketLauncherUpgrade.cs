using SaveData;

public class RocketLauncherUpgrade : Upgrade
{
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
}
