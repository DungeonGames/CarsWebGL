using UnityEngine;

public class SubaruItem : UnlockableItem
{
    [SerializeField] private Car _subaru;

    private const string CarSubaru = "CarSubaru";

    public override void Load()
    {
        var dataCar = SaveSystem.Load<SaveData.PlayerData>(CarSubaru);
        IsBuyed = dataCar.SubaruIsBuyed;
    }

    public override void Save()
    {
        SaveSystem.Save(CarSubaru, GetSaveSnapshot());
    }

    public override SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            SubaruIsBuyed = IsBuyed,
        };

        return data;
    }

    public override void ActivateItem() => PlayerBag.ActivateNewCar(_subaru);
}
