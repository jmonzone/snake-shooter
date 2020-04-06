using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    [Header("Manager References")]
    [SerializeField] private LevelManager levelManager;

    [Header("UI References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Button button;
    [SerializeField] private Text roundText;
    [SerializeField] private Text rewardText;


    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneNames.HOME_SCENE);
        });

        GameOverManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameOverManager.OnGameOver -= OnGameOver;

    }

    private void OnGameOver(GameOverEventArgs args)
    {
        if (args.CanRevive) return;

        display.SetActive(true);
        roundText.text = $"You have reached\nRound {levelManager.RoundIndex + 1} of {levelManager.RoundCount}";

    }
}
