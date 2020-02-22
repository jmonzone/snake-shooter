using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisabler : MonoBehaviour
{
    private ShopItemManager shopItemManager;


    private void Awake()
    {
        shopItemManager = GetComponent<ShopItemManager>();
        ShopItemManager.OnUnlockableTowerChanged += Disable;
    }

    private void Start()
    {
            Disable(shopItemManager.CurrentUnlockableTower);
    }

    private void Disable(UnlockableTower unlockableTower)
    {
        var playerOwnsUnlockable = PlayerPrefs.HasKey(unlockableTower.Key);

        shopItemManager.PurchaseButton.interactable = !playerOwnsUnlockable;

        var image = shopItemManager.ItemImage;
        var alpha = playerOwnsUnlockable ? 0.5f : 1;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

        shopItemManager.ItemText.text = playerOwnsUnlockable ? "ALREADY PURCHASED" : unlockableTower.Description; ;
    }
}
