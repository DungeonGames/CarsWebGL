using SaveData;
using UnityEngine;

public class LauncherItem : UnlockableItem
{
    [SerializeField] private Gun _rocketLauncher;

    private const string GunLauncher = "GunLauncher";

    public override void ActivateItem() => PlayerBag.ActivateNewGun(_rocketLauncher);

    public override PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            RocketLauncherIsBuyed = IsBuyed,
        };

        return data;
    }

    public override void Load()
    {
        var dataGun = SaveSystem.Load<SaveData.PlayerData>(GunLauncher);
        IsBuyed = dataGun.RocketLauncherIsBuyed;
    }

    public override void Save()
    {
        SaveSystem.Save(GunLauncher, GetSaveSnapshot());
    }
}
