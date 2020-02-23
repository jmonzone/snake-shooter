using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Linq;

public class ShopItemManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemText;
    [SerializeField] private Text itemPrice;
    [SerializeField] private RewardedAdsButton purchaseButton;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    private int unlockableTowerIndex;
    private UnlockableTower currentUnlockableTower;

    public Image ItemImage => itemImage;
    public Text ItemText => itemText;
    public RewardedAdsButton PurchaseButton => purchaseButton;

    public static event Action<UnlockableTower> OnUnlockableTowerChanged;
    public static event Action OnPurchase;

    private void Awake()
    {
        purchaseButton.OnAdFinished += () =>
        {
            AddTower(CurrentUnlockableTower);
        };

        nextButton.onClick.AddListener(() => SwitchUnlockableTower(true));
        previousButton.onClick.AddListener(() => SwitchUnlockableTower(false));

        CurrentUnlockableTower = GameManager.Instance.RemainingUnlockableTowers[0];
    }

    private void SwitchUnlockableTower(bool next = true)
    {
        var value = next ? unlockableTowerIndex + 1 : unlockableTowerIndex - 1;

        unlockableTowerIndex = Mathf.Clamp(value, 0, GameManager.Instance.RemainingUnlockableTowers.Count);
        CurrentUnlockableTower = GameManager.Instance.RemainingUnlockableTowers[unlockableTowerIndex];
    }

    private void AddTower(UnlockableTower unlockableTower)
    {
        if (Currency.Count >= unlockableTower.Price)
        {
            Currency.Count -= unlockableTower.Price;
            GameManager.Instance.AddAvailableTower(unlockableTower);
        }
    }

    public UnlockableTower CurrentUnlockableTower
    {
        get => currentUnlockableTower;
        private set
        {
            currentUnlockableTower = value;

            itemName.text = currentUnlockableTower.TowerName;
            itemImage.sprite = currentUnlockableTower.Sprite;
            itemText.text = currentUnlockableTower.Description;
            itemPrice.text = currentUnlockableTower.Price.ToString();

            previousButton.gameObject.SetActive(unlockableTowerIndex != 0);
            nextButton.gameObject.SetActive(unlockableTowerIndex != GameManager.Instance.RemainingUnlockableTowers.Count - 1);

            OnUnlockableTowerChanged?.Invoke(currentUnlockableTower);
        }
    }



}
