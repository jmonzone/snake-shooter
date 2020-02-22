using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeManager snakeManager;
    [SerializeField] private HomeHealthManager homeHealthManager;
    [SerializeField] private GameOverDisplay gameOverDisplay;

    public static event Action OnGameOver;
    public static event Action OnRevive;

    public static bool GameIsOver { get; private set; } = false;

    private void Start()
    {
        snakeManager.OnHeadDestroyed += GameOver;
        homeHealthManager.OnHealthZero += GameOver;
        gameOverDisplay.OnReviveButtonClicked += Revive;
    }

    private void GameOver()
    {
        GameIsOver = true;
        OnGameOver?.Invoke();
        
    }

    private void Revive()
    {
        GameIsOver = false;
        OnRevive?.Invoke();
    }
}
