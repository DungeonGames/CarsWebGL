using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Wallet))]
public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup _container;
    [SerializeField] private PlayerBag _playerBag;
    [SerializeField] private UpgradeView _template;

    private Wallet _wallet;
    private UpgradeView _carUpgradeView;
    private UpgradeView _gunUpgradeView;

    private void OnEnable()
    {
        _playerBag.CarChanged += OnCarChanged;
        _playerBag.GunChanged += OnGunChanged;
    }

    private void Start()
    {
        _wallet = GetComponent<Wallet>();
        InstantiateUpgrades();
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _playerBag.GunChanged -= OnGunChanged;
        _carUpgradeView.SellButtonClick -= OnSellButtonClick;
        _gunUpgradeView.SellButtonClick -= OnSellButtonClick;
    }

    private void OnCarChanged(Car car)
    {
        _carUpgradeView.Render(car.CarUpgrade);
        UpdateInteractableButton(_carUpgradeView, car.CarUpgrade);
    }

    private void OnGunChanged(Gun gun)
    {
        _gunUpgradeView.Render(gun.GunUpgrade);
        UpdateInteractableButton(_gunUpgradeView, gun.GunUpgrade);
    }

    private void InstantiateUpgrades()
    {
        AddUpgrades();
    }

    private void AddUpgrades()
    {
        _gunUpgradeView = Instantiate(_template, _container.transform);
        _carUpgradeView = Instantiate(_template, _container.transform);
        _gunUpgradeView.SellButtonClick += OnSellButtonClick;
        _carUpgradeView.SellButtonClick += OnSellButtonClick;
    }

    private void UpdateInteractableButton(UpgradeView upgradeView, Upgrade upgrade)
    {
        if (upgrade.Price > _wallet.Coins)
        {
            upgradeView.DeactivateButton();
        }
    }

    private void OnSellButtonClick(UpgradeView upgradeView)
    {
        TrySellUpgrade(upgradeView);
    }

    private void TrySellUpgrade(UpgradeView upgradeView)
    {
        if (_wallet.TryDecreaseCoins(upgradeView.Upgrade.Price))
        {
            upgradeView.Upgrade.SellUpgrade();
            _wallet.DecreaseCoins(upgradeView.Upgrade.Price);
            
        }

        UpdateInteractableButton(_carUpgradeView, _carUpgradeView.Upgrade);

        UpdateInteractableButton(_gunUpgradeView, _gunUpgradeView.Upgrade);
    }
}
