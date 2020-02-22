using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
   [Header("Options")]
   [SerializeField] private Color lowHealthColor;
   [SerializeField] private Color highHealthColor;

    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemy.Health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        spriteRenderer.color = Color.Lerp(lowHealthColor, highHealthColor, health / enemy.Health.Max);
    }
}
