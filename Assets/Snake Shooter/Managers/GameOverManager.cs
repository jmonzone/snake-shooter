using System;
using UnityEngine;

public struct GameOverEventArgs
{
    public int availableRevives;

    public bool CanRevive => availableRevives > 0;

    public GameOverEventArgs(int revives)
    {
        availableRevives = revives;
    }
}

public class GameOverManager : MonoBehaviour
{
    public static event Action<GameOverEventArgs> OnGameOver;
    public static event Action OnRevive;

    public static bool GameIsOver { get; private set; } = false;

    private int availableRevives = 0;
    private const int DEFAULT_AVAILABLE_REVIVES = 1;

    private void Awake()
    {
        availableRevives = DEFAULT_AVAILABLE_REVIVES;
    }

    private void OnEnable()
    {
        GameIsOver = false;

        SnakeManager.OnLastNodeDetatched += GameOver;
        ReviveDisplay.OnReviveButtonClicked += Revive;
    }

    private void OnDisable()
    {
        SnakeManager.OnLastNodeDetatched -= GameOver;
        ReviveDisplay.OnReviveButtonClicked -= Revive;
    }

    private void GameOver()
    {
        GameIsOver = true;

        var args = new GameOverEventArgs(availableRevives);

        OnGameOver?.Invoke(args);
    }

    private void Revive()
    {
        GameIsOver = false;

        availableRevives -= 1;

        Debug.Log($"Player has revived. Remaining Revives: {availableRevives}");
        OnRevive?.Invoke();
    }
}
