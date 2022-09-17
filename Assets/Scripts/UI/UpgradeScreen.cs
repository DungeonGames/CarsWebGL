using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup _container;
    [SerializeField] private Car _car;
    [SerializeField] private Gun _gun;
    [SerializeField] private UpgradeView _template;
    [SerializeField] private Wallet _wallet;

    private List<UpgradeView> _upgradeViews = new List<UpgradeView>();

    private void Start()
    {
        InstantiateUpgrades();
    }

    private void OnDisable()
    {
        foreach (var view in _upgradeViews)
        {
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

    private void InstantiateUpgrades()
    {
        AddUpgrade(_gun.GunUpgrade);
        AddUpgrade(_car.CarUpgrade);
        UpdateInteractableButtons();
    }

    private void AddUpgrade(Upgrade upgrade)
    {
        UpgradeView view = Instantiate(_template, _container.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(upgrade);
        _upgradeViews.Add(view);
    }

    private void UpdateInteractableButtons()
    {
        foreach (var view in _upgradeViews)
        {
            if (view.Upgrade.Price > _wallet.Coins || view.Upgrade.CanSellUpgrade() == false)
            {
                view.DeactivateButton();
            }
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
            if (upgradeView.Upgrade.CanSellUpgrade())
            {
                upgradeView.Upgrade.SellUpgrade();
                _wallet.DecreaseCoins(upgradeView.Upgrade.Price);
                UpdateInteractableButtons();
            }
        }
    }
}
