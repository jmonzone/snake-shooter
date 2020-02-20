using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameOverManager gameOverManager;

    [Header("Options")]
    [SerializeField] private List<Tower> snakeTowerPrefabs;

    public event Action<List<Tower>> OnUpgradesRandomlySelected;

    private void Start()
    {
        levelManager.OnLevelEnded += OnLevelEnded;
        gameOverManager.OnGameOver += () =>
        {

            levelManager.OnLevelEnded -= OnLevelEnded;
            gameOverManager.OnRevive += () => levelManager.OnLevelEnded += OnLevelEnded;

        };

    }

    private void OnLevelEnded()
    {
        var snakeTowers = new List<Tower>(snakeTowerPrefabs);
        var selectedUpgrades = new List<Tower>();

        for (int i = 0; i < 3; i++)
        {
            var rand = UnityEngine.Random.Range(0, snakeTowers.Count);
            var snakeTower = snakeTowers[rand];
            selectedUpgrades.Add(snakeTower);
            snakeTowers.Remove(snakeTower);
        }

        OnUpgradesRandomlySelected?.Invoke(selectedUpgrades);
    }
}