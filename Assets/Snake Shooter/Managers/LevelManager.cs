using System;
using System.Collections.Generic;
using UnityEngine;

public struct LevelCompleteEventArgs
{
    /// <summary>
    /// The number of scales rewarded to the player on level completion
    /// </summary>
    public int currencyReward;
}

public struct OnRoundBegunEventArgs
{
    public int currentRound;
    public int roundIndex;
    public int roundCount;
    public int enemyCount;
}

public class LevelManager : MonoBehaviour
{
    private ScriptableLevel currentLevel;

    private int enemiesDefeated = 0;
    public int RoundIndex { get; private set; } = -1;

    public Round CurrentRound => currentLevel.Rounds[RoundIndex];
    public int EnemyCount => CurrentRound.EnemyCount;
    public int RoundCount => currentLevel.RoundCount;
    public int CurrencyReward => currentLevel.Reward;

    public static event Action<int> OnRoundChanged;
    public static event Action<OnRoundBegunEventArgs> OnRoundBegun;
    public static event Action<int> OnRoundEnded;
    public static event Action<LevelCompleteEventArgs> OnLevelComplete;

    #region Monobehaviour
    private void OnEnable()
    {
        currentLevel = GameManager.Instance.CurrentLevel;

        EnemyManager.OnEnemyDefeated += OnEnemyDefeated;
        GameOverManager.OnRevive += OnRevive;
        UpgradeDisplay.OnUpgradeEnd += BeginRound;
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyDefeated -= OnEnemyDefeated;
        UpgradeDisplay.OnUpgradeEnd -= BeginRound;
        GameOverManager.OnRevive -= OnRevive;
    }
    #endregion

    #region Functionality
    private void BeginRound()
    {
        RoundIndex++;
        enemiesDefeated = 0;

        Debug.Log($"Round {RoundIndex + 1} has begun with {EnemyCount} enemies.");
        OnRoundBegun?.Invoke(OnRoundBegunEventArgs);
    }


    private void CompleteLevel()
    {
        Debug.Log("Level has been completed.");
        OnLevelComplete?.Invoke(LevelCompleteEventArgs);

        if (currentLevel.ID >= GameManager.Instance.HighestLevel)
            GameManager.Instance.HighestLevel++;
    }
    #endregion

    #region Event Listeners
    private void OnEnemyDefeated(GameObject enemy)
    {
        enemiesDefeated++;

        if (enemiesDefeated < EnemyCount) return;
        Debug.Log("All enemies have been defeated. Round has ended.");

        if (RoundIndex + 1 == RoundCount) CompleteLevel();
        else OnRoundEnded?.Invoke(RoundIndex);
    }

    private void OnRevive()
    {
        enemiesDefeated = 0;
        OnRoundBegun?.Invoke(OnRoundBegunEventArgs);
    }
    #endregion

    #region Event Args
    private OnRoundBegunEventArgs OnRoundBegunEventArgs
    {
        get
        {
            return new OnRoundBegunEventArgs()
            {
                currentRound = RoundIndex + 1,
                roundIndex = RoundIndex,
                roundCount = RoundCount,
                enemyCount = EnemyCount
            };
        }
    }

    private LevelCompleteEventArgs LevelCompleteEventArgs
    {
        get
        {
            return new LevelCompleteEventArgs()
            {
                currencyReward = CurrencyReward
            };
        }
    }
    #endregion
}
