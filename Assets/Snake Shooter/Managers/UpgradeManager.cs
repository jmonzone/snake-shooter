using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static event Action<List<ScriptableTower>> OnUpgradesRandomlySelected;

    private void Start()
    {
        LevelManager.OnLevelEnded += OnLevelEnded;
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelEnded -= OnLevelEnded;
    }

    private void OnLevelEnded(int level)
    {
        SelectUpgrades();
    }

    private void SelectUpgrades()
    {
        var snakeTowers = new List<ScriptableTower>(GameManager.Instance.availableTowers);
        var selectedUpgrades = new List<ScriptableTower>();

        for (int i = 0; i < snakeTowers.Count; i++)
        {
            selectedUpgrades.Add(snakeTowers[i]);
        }

        OnUpgradesRandomlySelected?.Invoke(selectedUpgrades);
    }
}