using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeManager snakeManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameObject display;
    [SerializeField] private List<Button> buttons;

    public event Action OnUpgradeSelected;

    private void Start()
    {
        upgradeManager.OnUpgradesRandomlySelected += InitButtons;
    }

    private void InitButtons(List<Tower> snakeTowers)
    {
        Display();

        int i = 0;

        snakeTowers.ForEach((snakeTower) =>
        {
            buttons[i].onClick.AddListener(() =>
            {
                snakeManager.AddSnakeTower(snakeTower);
                Display(false);
                ClearButtons();
                OnUpgradeSelected?.Invoke();
            });

            var text = buttons[i].GetComponentInChildren<Text>();
            text.text = snakeTower.Description;

            var image = buttons[i].GetComponentsInChildren<Image>()[1];
            image.sprite = snakeTower.Sprite;
            image.color = snakeTower.Color;
            i++;
        });
    }

    public void Display(bool show = true)
    {
        display.SetActive(show);
    }

    private void ClearButtons()
    {
        buttons.ForEach((button) =>
        {
            button.onClick.RemoveAllListeners();
        });
    }
}
