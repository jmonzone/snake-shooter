using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Tower", menuName = "ScriptableObjects/Scriptable Tower", order = 1)]
public class ScriptableTower : ScriptableObject
{
    [SerializeField] private string towerName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string description;
    [SerializeField] private Tower prefab;

    public Sprite Sprite => sprite;
    public string TowerName => towerName;
    public string Description => description;
    public Tower Prefab => prefab;
}
