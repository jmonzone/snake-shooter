using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private int health;

    public event Action<int> OnHealthChanged;
    public event Action OnHealthZero;


    public int Health
    {
        get => health;
        private set
        {
            if (value <= 0) health = 0;
            else health = value;

            OnHealthChanged?.Invoke(health);

            if(health == 0)
            {
                OnHealthZero?.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health--;
        }
    }
}
