using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A set of unlockable tower scriptable objects
/// </summary>
[CreateAssetMenu(fileName = "Unlockable Tower Set", menuName = "ScriptableObjects/Unlockable Tower Set", order = 3)]
public class UnlockableTowersSet : ScriptableObject
{
    [SerializeField] public List<UnlockableTower> List { get; private set; }
}
