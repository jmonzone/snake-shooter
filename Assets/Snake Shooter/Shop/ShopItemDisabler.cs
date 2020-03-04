using UnityEngine;

public class ShopItemDisabler : MonoBehaviour
{
    private ShopItemManager shopItemManager;

    private void Awake()
    {
        shopItemManager = GetComponent<ShopItemManager>();
    }

    private void OnEnable()
    {
        ShopItemManager.OnUnlockableTowerChanged += Disable;
        ShopItemManager.OnAllUnlocked += DisplayEmptyShop;
    }

    private void OnDisable()
    {
        ShopItemManager.OnUnlockableTowerChanged -= Disable;
        ShopItemManager.OnAllUnlocked -= DisplayEmptyShop;
    }

    private void DisplayEmptyShop()
    {
        shopItemManager.ItemName.text = "CONGRATS!";
        shopItemManager.ItemText.text = "YOU HAVE UNLOCKED EVERY TOWER!";


        shopItemManager.ItemImage.gameObject.SetActive(false);
        shopItemManager.ItemPrice.gameObject.SetActive(false);
        shopItemManager.PurchaseButton.gameObject.SetActive(false);

        shopItemManager.NextButton.gameObject.SetActive(false);
        shopItemManager.PreviousButton.gameObject.SetActive(false);

    }

    private void Disable(ScriptableTower unlockableTower)
    {
        var playerOwnsUnlockable = PlayerPrefs.HasKey(unlockableTower.Key);

        shopItemManager.PurchaseButton.Button.interactable = GameManager.Instance.CurrencyCount >= unlockableTower.UnlockPrice;

        var image = shopItemManager.ItemImage;
        var alpha = playerOwnsUnlockable ? 0.5f : 1;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

        shopItemManager.ItemText.text = playerOwnsUnlockable ? "ALREADY PURCHASED" : unlockableTower.Description; ;
    }
}
