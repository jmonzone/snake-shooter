using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float baseMax = 3.0f;
    [SerializeField] private float growth = 0.25f;

    public Action<float> OnHealthChanged;
    public Action OnHealthZero;

    private float value = 3.0f;
    public float Value
    {
        get => value;
        set
        {
            var newValue = Mathf.Clamp(value, 0, Max);

            if (newValue == this.value) return;

            this.value = newValue;
            OnHealthChanged?.Invoke(this.value);

            if (this.value == 0)
            {
                OnHealthZero?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    public float Max { get; set; }

    public float BaseMax => baseMax;
    public float Growth => growth;

    private void Awake()
    {
        value = BaseMax;
        Max = BaseMax; 
    }

    private void OnEnable()
    {
        Value = Max;
    }

    public void AddOvertimeEffect(Action effect, float duration)
    {
        if(gameObject.activeSelf)
            StartCoroutine(EffectUpdate(effect, duration));
    }

    private IEnumerator EffectUpdate(Action effect, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            if (gameObject.activeSelf)
                effect();
            yield return new WaitForSeconds(1.0f);
        }

    }

}