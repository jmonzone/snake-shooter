using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Enemy", menuName = "ScriptableObjects/Scriptable Enemy", order = 5)]
public class ScriptableEnemy : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private GameObject prefab;

    public string ID => id;
    public GameObject Prefab => prefab;
}
