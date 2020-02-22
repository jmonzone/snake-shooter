using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private List<ScriptableTower> towers;
    [SerializeField] private List<UnlockableTower> unlockables;

    public static event Action<List<ScriptableTower>> OnUpgradesRandomlySelected;

    private void Awake()
    {
        unlockables.ForEach((unlockable) =>
        {
            if (PlayerPrefs.GetInt(unlockable.Key) == 1)
            {
                towers.Add(unlockable);
            }
        });
    }

    private void Start()
    {
        LevelManager.OnLevelEnded += OnLevelEnded;
    }

    private void OnLevelEnded(int level)
    {
        SelectUpgrades();
    }

    private void SelectUpgrades()
    {
        var snakeTowers = new List<ScriptableTower>(towers);
        var selectedUpgrades = new List<ScriptableTower>();

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