using UnityEngine;
using UnityEngine.Events;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] private string _upgradeName;
    [SerializeField] private Sprite _imageUpgrade;
    [SerializeField] private int _price;

    protected int _currentLevel;

    public event UnityAction Buyed;
    public event UnityAction<int> CurrentLevelChanged;

    public string UpgradeName => _upgradeName;
    public Sprite ImageUpgrade => _imageUpgrade;
    public int Price => _price;
    public int CurrentLevel => _currentLevel;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        CurrentLevelChanged?.Invoke(_currentLevel);
    }

    public abstract void Load();

    public abstract void Save();

    public abstract SaveData.PlayerData GetSaveSnapshot();

    public void SellUpgrade()
    {
        _currentLevel++;
        Buyed.Invoke();
        Save();
    }
}
