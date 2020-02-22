using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int progress = 0;
    private int goal = 1;
    private int level = 1;

    public static event Action<int> OnProgressChanged;
    public static event Action<int> OnGoalChanged;
    public static event Action<int> OnLevelChanged;
    public static event Action<int> OnLevelBegun;
    public static event Action<int> OnLevelEnded;

    private void Start()
    {
        EnemyManager.OnEnemyDefeated += OnEnemyDefeated;
        EnemyManager.OnEnemyHasReachedBase += OnEnemyDefeated;

        UpgradeDisplay.OnUpgradeSelected += OnUpgradeSelected;
        GameOverManager.OnRevive += OnRevive;
        GameOverManager.OnGameOver += OnGameOver;

        OnLevelBegun?.Invoke(Level);
    }

    #region Event Listeners
    private void OnEnemyDefeated(Enemy enemy)
    {
        Progress++;

        if (Progress == Goal)
        {
            OnLevelEnded?.Invoke(Level);
        }
    }

    private void OnUpgradeSelected(Tower tower)
    {
        Progress = 0;
        Level += 1;
        Goal = Level;

        OnLevelBegun?.Invoke(Level);
    }

    private void OnGameOver()
    {
        EnemyManager.OnEnemyHasReachedBase -= OnEnemyDefeated;
    }

    private void OnRevive()
    {
        EnemyManager.OnEnemyHasReachedBase += OnEnemyDefeated;

        Progress = 0;
        OnLevelBegun?.Invoke(Level);
    }
    #endregion

    #region Properties
    public int Progress
    {
        get => progress;
        set
        {
            progress = Mathf.Clamp(value, 0, goal);
            OnProgressChanged?.Invoke(progress);
        }
    }

    public int Goal
    {
        get => goal;
        set
        {
            goal = value;
            OnGoalChanged?.Invoke(goal);
        }
    }

    public int Level
    {
        get => level;
        set
        {
            level = value;
            Goal = level;

            OnLevelChanged?.Invoke(Goal);
        }
    }
    #endregion
}
