using UnityEngine;
using UnityEngine.UI;

public class HomeHealthDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HomeHealthManager homeHealthManager;

    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        homeHealthManager.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        text.text = $"HP: {health}";
    }
}
