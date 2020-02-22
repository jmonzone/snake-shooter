using UnityEngine;

[CreateAssetMenu(fileName = "Unlockable Tower", menuName = "ScriptableObjects/Unlockable Tower", order = 2)]
public class UnlockableTower : ScriptableTower
{
    [SerializeField] private string key;
    [SerializeField] private int price;

    public string Key => key;
    public int Price => price;


}
