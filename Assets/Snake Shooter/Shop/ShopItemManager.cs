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
    private ScriptableTower currentUnlockableTower;

    public Text ItemName => itemName;
    public Image ItemImage => itemImage;
    public Text ItemText => itemText;
    public Text ItemPrice => itemPrice;
    public RewardedAdsButton PurchaseButton => purchaseButton;
    public Button NextButton => nextButton;
    public Button PreviousButton => previousButton;

    public static event Action<ScriptableTower> OnUnlockableTowerChanged;
    public static event Action OnUnlockablePurchased;
    public static event Action OnAllUnlocked;

    private void Awake()
    {
        purchaseButton.Button.onClick.AddListener(() => AddTower(CurrentUnlockableTower));

        nextButton.onClick.AddListener(() => SwitchUnlockableTower(true));
        previousButton.onClick.AddListener(() => SwitchUnlockableTower(false));
    }

    private void Start()
    {
        if (GameManager.Instance.RemainingUnlockableTowers.Count == 0)
            OnAllUnlocked?.Invoke();
        else 
            CurrentUnlockableTower = GameManager.Instance.RemainingUnlockableTowers[0];
    }

    private void SwitchUnlockableTower(bool next = true)
    {
        var value = next ? unlockableTowerIndex + 1 : unlockableTowerIndex - 1;

        unlockableTowerIndex = Mathf.Clamp(value, 0, GameManager.Instance.RemainingUnlockableTowers.Count);

        if (GameManager.Instance.RemainingUnlockableTowers.Count == 0)
        {
            OnAllUnlocked?.Invoke();
        }
        else
        {
            CurrentUnlockableTower = GameManager.Instance.RemainingUnlockableTowers[unlockableTowerIndex];
        }
    }

    private void AddTower(ScriptableTower unlockableTower)
    {
        if (GameManager.Instance.CurrencyCount >= unlockableTower.UnlockPrice)
        {
            GameManager.Instance.CurrencyCount -= unlockableTower.UnlockPrice;
            GameManager.Instance.AddAvailableTower(unlockableTower);
            SwitchUnlockableTower(false);
        }
    }

    public ScriptableTower CurrentUnlockableTower
    {
        get => currentUnlockableTower;
        private set
        {
            currentUnlockableTower = value;

            itemName.text = currentUnlockableTower.TowerName;
            itemImage.sprite = currentUnlockableTower.Sprite;
            itemText.text = currentUnlockableTower.Description;
            itemPrice.text = currentUnlockableTower.UnlockPrice.ToString();

            previousButton.gameObject.SetActive(unlockableTowerIndex != 0);
            nextButton.gameObject.SetActive(unlockableTowerIndex != GameManager.Instance.RemainingUnlockableTowers.Count - 1);

            OnUnlockableTowerChanged?.Invoke(currentUnlockableTower);
        }
    }



}
