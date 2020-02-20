using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SnakeManager snakeManager;
    [SerializeField] private UpgradeDisplay upgradeDisplay;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private GameOverDisplay gameOverDisplay;

    public event Action OnGameOver;
    public event Action OnRevive;

    private void Start()
    {
        snakeManager.OnHeadDestroyed += GameOver;
        healthManager.OnHealthZero += GameOver;
        gameOverDisplay.OnReviveButtonClicked += Revive;
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
        upgradeDisplay.Display(false);
    }

    private void Revive()
    {
        OnRevive?.Invoke();
    }
}
