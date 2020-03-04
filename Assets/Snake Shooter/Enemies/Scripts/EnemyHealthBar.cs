using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject display;
    [SerializeField] private Transform pivot;
    [SerializeField] private SpriteRenderer fill;

    private EnemyHealth health;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= OnHealthChanged;

        display.SetActive(false);
    }

    private void OnHealthChanged(float currentHealth)
    {
        display.SetActive(true);

        var value = currentHealth / health.Max;

        var newScale = Vector3.one;
        newScale.x = value;
        pivot.transform.localScale = newScale;


        fill.color = Color.Lerp(Color.red, Color.green, value);
    }
}
