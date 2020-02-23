using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
   [Header("Options")]
   [SerializeField] private Color lowHealthColor;
   [SerializeField] private Color highHealthColor;

    private Health health;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.color = Color.Lerp(lowHealthColor, highHealthColor, health.Value / health.Max);
    }
}
