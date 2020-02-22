using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ShopItemManager : MonoBehaviour
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else Debug.Log("Advertisement not ready");
    }

    [Header("References")]
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemText;
    [SerializeField] private Text itemPrice;
    [SerializeField] private Button purchaseButton;


    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    [Header("Options")]
    [SerializeField] private List<UnlockableTower> unlockableTowers;

    public Image ItemImage => itemImage;
    public Text ItemText => itemText;
    public Button PurchaseButton => purchaseButton;

    private int unlockableTowerIndex;

    private UnlockableTower currentUnlockableTower;
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
            nextButton.gameObject.SetActive(unlockableTowerIndex != unlockableTowers.Count - 1);

            OnUnlockableTowerChanged?.Invoke(currentUnlockableTower);
        }
    }

    public static event Action<UnlockableTower> OnPurchaseButtonClicked;
    public static event Action<UnlockableTower> OnUnlockableTowerChanged;


    private void Awake()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            ShowAd();
            OnPurchaseButtonClicked?.Invoke(CurrentUnlockableTower);
        });

        nextButton.onClick.AddListener(() => SwitchUnlockableTower(true));
        previousButton.onClick.AddListener(() => SwitchUnlockableTower(false));

        CurrentUnlockableTower = unlockableTowers[0];
    }

    private void SwitchUnlockableTower(bool next = true)
    {
        var value = next ? unlockableTowerIndex + 1 : unlockableTowerIndex - 1;

        unlockableTowerIndex = Mathf.Clamp(value, 0, unlockableTowers.Count);


        CurrentUnlockableTower = unlockableTowers[unlockableTowerIndex];
    }



}
