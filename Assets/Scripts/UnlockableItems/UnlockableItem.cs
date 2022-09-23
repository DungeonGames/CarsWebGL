using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class UnlockableItem : MonoBehaviour
{
    [SerializeField] private protected PlayerBag PlayerBag;

    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _currentQuanityText;
    [SerializeField] private int _quanity;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;
    [SerializeField] private int _price;

    private protected bool IsBuyed = false;

    private bool _isUnlock = false;

    public int Quanity => _quanity;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public string Description => _description;
    public int Price => _price;
    public bool IsBuyedItem => IsBuyed;
    public bool IsUnlock => _isUnlock;

    public event UnityAction<UnlockableItem> Unlock;

    public abstract void Load();

    public abstract void Save();

    public abstract void ActivateItem();

    public abstract SaveData.PlayerData GetSaveSnapshot();

    public void ShowProgress(int currentQuanity)
    {
        _progressBar.value = (float)currentQuanity / _quanity;

        if (_quanity - currentQuanity == 0)
        {
            _currentQuanityText.text = "UNLOCKED";
        }
        else
        {
            _currentQuanityText.text = $"{_quanity - currentQuanity} Waves to Unlock";
        }
    }

    public void Unlocked()
    {
        _isUnlock = true;
        Unlock?.Invoke(this);
        Load();
    }

    public void Buyed()
    {
        IsBuyed = true;
        Save();
    }
}
