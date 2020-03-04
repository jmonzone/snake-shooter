using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Tower", menuName = "ScriptableObjects/Scriptable Tower", order = 1)]
public class ScriptableTower : ScriptableObject, IComparable<ScriptableTower>
{
    [SerializeField] private string towerName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string description;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int price;
    [SerializeField] private int unlockPrice;

    public Sprite Sprite => sprite;
    public string TowerName => towerName;
    public string Description => description;
    public GameObject Prefab => prefab;
    public string Key => towerName;

    public int Price => price;
    public int UnlockPrice => unlockPrice;

    public int CompareTo(ScriptableTower other)
    {
        if (!other)
            return 1;
        else
            return Price.CompareTo(other.Price);
    }
}
