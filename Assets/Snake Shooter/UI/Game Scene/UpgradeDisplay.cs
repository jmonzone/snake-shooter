using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;

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
        Invoke(nameof(Display), 0.5f);
    }

    private void OnAllCurrencyObjectsCollected()
    {
        Display();
    }

    private void Display()
    {
        Debug.Log("Displaying upgrades.");
        display.SetActive(true);
    }

    private void Init()
    {
        //get all upgrade buttons in children
        GetComponentsInChildren(includeInactive: true, upgradeButtons);

        //disable all upgrade buttons
        upgradeButtons.ForEach(button => button.gameObject.SetActive(false));

        //get available towers and sort
        var towers = GameManager.Instance.AvailableTowers;
        towers.Sort();

        for (int i = 0; i < towers.Count && i < upgradeButtons.Count; i++)
        {
            var tower = towers[i];
            Debug.Log(tower);
            var upgradeButton = upgradeButtons[i];

            upgradeButton.gameObject.SetActive(true);

            upgradeButton.Button.onClick.AddListener(() =>
            {
                OnUpgradeSelected?.Invoke(tower);
                Display(false);
            });

            upgradeButton.TowerNameText.text = tower.TowerName;
            upgradeButton.TowerImage.sprite = tower.Sprite;
        }
    }

    private void Display(bool show = true)
    {
        Debug.Log("Upgrade display: display = " + show);

        display.gameObject.SetActive(show);
        if (!show)
        {
            OnUpgradeEnd?.Invoke();
        }
    }
}
