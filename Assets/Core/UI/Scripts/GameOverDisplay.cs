using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameOverManager gameOverManager;
    [SerializeField] private GameObject display;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button revive;

    public event Action OnReviveButtonClicked;


    private void Start()
    {
        gameOverManager.OnGameOver += OnGameOver;
        playAgain.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game Scene");
        });

        revive.onClick.AddListener(() =>
        {
            OnReviveButtonClicked?.Invoke();
            display.SetActive(false);
        });
    }

    private void OnGameOver()
    {
        display.SetActive(true);
    }
}
