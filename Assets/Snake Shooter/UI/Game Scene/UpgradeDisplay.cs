using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Button continueButton;

    private readonly List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();

    public static event Action<ScriptableTower> OnUpgradeSelected;
    public static event Action OnUpgradeEnd;

    private void OnEnable()
    {
        Init();

        TutorialManager.OnTutorialEnd += OnTutorialEnd;
        CurrencyObjectManager.OnAllCurrencyObjectsCollected += OnAllCurrencyObjectsCollected;
    }

    private void OnDisable()
    {
        TutorialManager.OnTutorialEnd -= OnTutorialEnd;
        CurrencyObjectManager.OnAllCurrencyObjectsCollected -= OnAllCurrencyObjectsCollected;
    }

    private void OnTutorialEnd()
    {
        Invoke(nameof(Display), 1.0f);
    }

    private void OnAllCurrencyObjectsCollected()
    {
        Display();
    }

    private void Display()
    {
        Debug.Log("Displaying upgrades.");
        display.SetActive(true);
        UpdateUpgradeDisplay();
    }

    private void UpdateUpgradeDisplay()
    {
        var towers = GameManager.Instance.AvailableTowers;
        towers.Sort();

        for (int i = 0; i < towers.Count; i++)
        {
            var tower = towers[i];
            var button = upgradeButtons[i];

            var purchasable = tower.Price <= CurrencyManager.Instance.CurrencyCount;

            button.PurchaseButton.interactable = purchasable;

            button.TowerNameText.text = tower.TowerName;
            button.TowerImage.color = purchasable ? Color.white : new Color(255, 255, 255, 0.5f);
        }
    }

    private void Init()
    {
        GetComponentsInChildren(includeInactive: true, upgradeButtons);
        upgradeButtons.ForEach(button => button.gameObject.SetActive(false));

        var towers = GameManager.Instance.AvailableTowers;
        towers.Sort();

        for (int i = 0; i < towers.Count; i++)
        {
            var tower = towers[i];
            var button = upgradeButtons[i];

            button.gameObject.SetActive(true);

            button.PurchaseButton.onClick.AddListener(() =>
            {
                OnUpgradeSelected?.Invoke(tower);
                UpdateUpgradeDisplay();
            });

            button.TowerNameText.text = tower.TowerName;
            button.TowerImage.sprite = tower.Sprite;
            button.PriceText.text = tower.Price.ToString();
        }

        continueButton.onClick.AddListener(() =>
        {
            display.gameObject.SetActive(false);
            OnUpgradeEnd?.Invoke();
        });
    }
}
