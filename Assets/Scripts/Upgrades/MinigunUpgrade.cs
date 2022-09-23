using SaveData;

public class MinigunUpgrade : Upgrade
{
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
    }

    public override void Save()
    {
        SaveSystem.Save(MinigunLevel, GetSaveSnapshot());
    }
}
