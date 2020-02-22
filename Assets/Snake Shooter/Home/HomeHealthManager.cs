using System;
using UnityEngine;

public class HomeHealthManager : MonoBehaviour
{
    private int maxHealth = 10;
    private int currentHealth = 10;

    public event Action<int> OnHealthChanged;
    public event Action OnHealthZero;
    public event Action OnEnemyHasReachedBase;

    private void Start()
    {
        GameOverManager.OnRevive += ReplenishHealth;

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
            OnEnemyHasReachedBase?.Invoke();
        }
    }
}
