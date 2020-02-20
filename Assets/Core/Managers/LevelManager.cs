using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Refrerences")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UpgradeDisplay upgradeDisplay;
    [SerializeField] private GameOverManager gameOverManager;

    private int progress = 0;
    private int goal = 1;
    private int level = 1;

    public int Progress
    {
        get => progress;
        set
        {
            progress = Mathf.Clamp(value, 0, goal);
            OnProgressChanged?.Invoke(progress);

            if(progress == goal)
            {
                Progress = 0;
                OnLevelEnded?.Invoke();
            }

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
            OnLevelBegun?.Invoke();
        }
    }

    public Action<int> OnProgressChanged;
    public Action<int> OnGoalChanged;
    public Action<int> OnLevelChanged;

    public Action OnLevelEnded;
    public Action OnLevelBegun;

    private void Start()
    {
        enemyManager.OnEnemyDefeated += OnEnemyDefeated;
        upgradeDisplay.OnUpgradeSelected += OnUpgradeSelected;
        gameOverManager.OnRevive += OnRevive;

        OnLevelBegun?.Invoke();
    }

    private void OnRevive()
    {
        Level = Level;
    }

    private void OnEnemyDefeated()
    {
        Progress++;
    }

    private void OnUpgradeSelected()
    {
        Level++;
    }
}
