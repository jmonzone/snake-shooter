using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelManager levelManager;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        levelManager.OnLevelChanged += OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        text.text = $"LV. {level}";
    }
}
