using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private List<UnlockableItem> _unlockableItems;
    [SerializeField] private ItemView _template;
    [SerializeField] private Wallet _wallet;

    private List<ItemView> _itemViews = new List<ItemView>();

    private void Start()
    {
        for (int i = 0; i < _unlockableItems.Count; i++)
        {
                AddItem(_unlockableItems[i]);
        }

        UpdateInteractableButtons();
    }

    private void AddItem(UnlockableItem unlockableItem)
    {
        ItemView view = Instantiate(_template, transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(unlockableItem);
        _itemViews.Add(view);
    }

    private void UpdateInteractableButtons()
    {
        if (_itemViews != null)
        {
            foreach (var view in _itemViews)
            {
                if (view.UnlockableItem.IsBuyedItem)
                {
                    view.SellItem();
                }
                else
                {
                    if (view.UnlockableItem.Price > _wallet.Gems)
                    {
                        view.DeactivateButton();
                    }
                }
            }
        }
    }

    private void OnSellButtonClick(ItemView itemView)
    {
        if (itemView.UnlockableItem.IsBuyedItem == false)
        {
            TrySellItem(itemView);
        }
    }

    private void TrySellItem(ItemView itemView)
    {
        if (_wallet.TryDecreaseGems(itemView.UnlockableItem.Price))
        {
            itemView.SellItem();
            _wallet.DecreaseGems(itemView.UnlockableItem.Price);
        }
        UpdateInteractableButtons();
    }
}
