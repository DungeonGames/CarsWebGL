using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private string _upgradeName;
    [SerializeField] private Sprite _imageUpgrade;
    [SerializeField] private int _price;
    [SerializeField] private int _maxQuanity;

    private int _currentQuanity;

    public event UnityAction Buyed;

    public string UpgradeName => _upgradeName;
    public Sprite ImageUpgrade => _imageUpgrade;
    public int Price => _price;
    public int MaxQuanity => _maxQuanity;
    public int CurrentQuanity => _currentQuanity;

    public bool CanSellUpgrade()
    {
        return _currentQuanity < _maxQuanity;
    }

    public void SellUpgrade()
    {
        if (CanSellUpgrade())
        {
            _currentQuanity++;
            Buyed.Invoke();
        }
    }
}
