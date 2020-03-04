using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager: MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool testMode;
    public bool TestMode => testMode;

    [SerializeField] private int startingCurrency;
    public int CurrencyCount
    {
        get => PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENCY_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENCY_KEY, value);
            OnCurrencyCountChanged?.Invoke(value);
        }
    }

    [SerializeField] private ScriptableLevel startingLevel;

    private ScriptableLevel currentLevel;
    public ScriptableLevel CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;
            OnCurrentLevelChanged?.Invoke(currentLevel);
        }
    }

    public int HighestLevel
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKeys.HIGHEST_LEVEL_KEY, 1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.HIGHEST_LEVEL_KEY, value);
        }
    }


    [SerializeField] private List<ScriptableLevel> availableLevels = new List<ScriptableLevel>();
    public List<ScriptableLevel> AvailableLevels => availableLevels;

    /// <summary>
    /// The set of towers available in the upgrade menu
    /// </summary>
    [SerializeField] private List<ScriptableTower> availableTowers = new List<ScriptableTower>();
    public List<ScriptableTower> AvailableTowers => availableTowers;

    /// <summary>
    /// The full list of unlockable towers
    /// </summary>
    [SerializeField] public List<ScriptableTower> allUnlockableTowers;

    public List<ScriptableTower> RemainingUnlockableTowers
    {
        get
        {
            var unlockables =  allUnlockableTowers.Where(unlockableTower => !availableTowers.Contains(unlockableTower)).ToList();
            unlockables.Sort();
            return unlockables;
        }
    }

    public event Action<int> OnCurrencyCountChanged;
    public static event Action<ScriptableLevel> OnCurrentLevelChanged;

    private void Awake(){

        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        foreach (ScriptableTower unlockableTower in allUnlockableTowers)
        {
            //try loading from player prefs
            int i = PlayerPrefs.GetInt(unlockableTower.Key);
            if (i == 1)
            {
                availableTowers.Add(unlockableTower);
            }
        }

        if (CurrencyCount == 0) CurrencyCount = startingCurrency;
        if (CurrentLevel == null) CurrentLevel = startingLevel;
    }

    public void AddAvailableTower(ScriptableTower tower)
    {
        availableTowers.Add(tower);
        PlayerPrefs.SetInt(tower.Key, 1);
    }

    public void ResetAvailableTowers()
    {
        var temp = AvailableTowers[0];
        availableTowers = new List<ScriptableTower>() { temp };
    }

}
