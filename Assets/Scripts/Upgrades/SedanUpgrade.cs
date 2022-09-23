using SaveData;

public class SedanUpgrade : Upgrade
{
    private const string SedanLevel = "SedanLevel";

    public override PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            SedanLevel = _currentLevel,
        };

        return data;
    }

    public override void Load()
    {
        var dataCar = SaveSystem.Load<SaveData.PlayerData>(SedanLevel);
        _currentLevel = dataCar.SedanLevel;
    }

    public override void Save()
    {
        SaveSystem.Save(SedanLevel, GetSaveSnapshot());
    }
}
