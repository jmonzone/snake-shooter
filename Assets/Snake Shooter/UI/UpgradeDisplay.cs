using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Transform buttonsParent;

    private readonly List<Button> buttons = new List<Button>();

    public static event Action<Tower> OnUpgradeSelected;
    public static event Action<bool> OnDisplayChanged;

    private void Awake()
    {
        buttonsParent.GetComponentsInChildren(buttons);

        UpgradeManager.OnUpgradesRandomlySelected += InitButtons;
        GameOverManager.OnGameOver += OnGameOver;
    }

    private void InitButtons(List<ScriptableTower> towers)
    {
        Display();

        int i = 0;

        towers.ForEach((tower) =>
        {
            buttons[i].onClick.AddListener(() =>
            {
                Display(false);
                ClearButtons();
                OnUpgradeSelected?.Invoke(tower.Prefab);
            });

            var towerNameText = buttons[i].transform.GetChild(0).GetComponent<Text>();
            towerNameText.text = tower.TowerName;

            var image = buttons[i].GetComponentsInChildren<Image>()[1];
            image.sprite = tower.Sprite;
            image.color = tower.Prefab.Color;

            var towerDescriptionText = buttons[i].transform.GetChild(2).GetComponent<Text>();
            towerDescriptionText.text = tower.Description;

            i++;
        });
    }

    private void OnGameOver()
    {
        Display(false);
    }

    private void Display(bool show = true)
    {
        if (show && GameOverManager.GameIsOver) return;

        display.SetActive(show);
        OnDisplayChanged?.Invoke(show);
    }

    private void ClearButtons()
    {
        buttons.ForEach((button) =>
        {
            button.onClick.RemoveAllListeners();
        });
    }
}
