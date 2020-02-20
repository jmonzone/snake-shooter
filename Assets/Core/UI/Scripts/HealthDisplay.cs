using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthManager healthManager;

    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        healthManager.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        text.text = health.ToString();
    }
}
