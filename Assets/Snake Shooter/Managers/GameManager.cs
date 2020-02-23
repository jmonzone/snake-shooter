using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    /// <summary>
    /// The set of towers available in the upgrade menu
    /// </summary>
    [SerializeField] public List<ScriptableTower> availableTowers = new List<ScriptableTower>();

    /// <summary>
    /// The full list of unlockable towers
    /// </summary>
    [SerializeField] public List<UnlockableTower> allUnlockableTowers;

    public List<UnlockableTower> RemainingUnlockableTowers
    {
        get
        {
            return allUnlockableTowers.Where(unlockableTower => !availableTowers.Contains(unlockableTower)).ToList();
        }
    }

    private void Awake(){
        DontDestroyOnLoad(this);
        Instance = this;

        foreach (UnlockableTower unlockableTower in allUnlockableTowers)
        {
            //try loading from player prefs
            int i = PlayerPrefs.GetInt(unlockableTower.Key);
            if (i == 1)
            {
                availableTowers.Add(unlockableTower);
            }
        }
    }

    public void AddAvailableTower(UnlockableTower tower)
    {
        availableTowers.Add(tower);
        PlayerPrefs.SetInt(tower.Key, 1);
    }
}
