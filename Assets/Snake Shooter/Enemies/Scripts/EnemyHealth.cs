using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
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

    public float Max { get; private set; }

    private void Awake()
    {
        value = baseMax;
        Max = baseMax;
        LevelManager.OnRoundBegun += UpdateStat;
    }

    private void OnDestroy()
    {
        LevelManager.OnRoundBegun -= UpdateStat;
    }

    private void UpdateStat(OnRoundBegunEventArgs args)
    {
        Max = baseMax + growth * (args.roundIndex);
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