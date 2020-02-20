using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameOverManager gameOverManager;

    private int maxHealth = 10;

    private int currentHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnHealthZero;

    private void Start()
    {
        gameOverManager.OnRevive += ReplenishHealth;

        Health = maxHealth;
    }

    public int Health
    {
        get => currentHealth;
        private set
        {
            if (value <= 0) currentHealth = 0;
            else currentHealth = value;

            OnHealthChanged?.Invoke(currentHealth);

            if(currentHealth == 0)
            {
                OnHealthZero?.Invoke();
            }
        }
    }

    private void ReplenishHealth()
    {
        Health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health--;
        }
    }
}
