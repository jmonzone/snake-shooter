using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    private List<StatusEffect> statusEffects;

    private Enemy enemy;

    private Action onDisable;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        statusEffects = new List<StatusEffect>();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    public void Add(StatusEffect statusEffect, Action onStatusRemoved = null)
    {
        statusEffects.Add(statusEffect);
        statusEffect.Afflict(enemy);

        onDisable += onStatusRemoved;

        if (gameObject.activeSelf)
            StartCoroutine(StatusUpdate(statusEffect, onStatusRemoved));
    }

    public IEnumerator StatusUpdate(StatusEffect statusEffect, Action onStatusRemoved = null)
    {
        yield return new WaitForSeconds(statusEffect.Duration);
        RemoveModifier(statusEffect, onStatusRemoved);
    }

    public void RemoveModifier(StatusEffect statusEffect, Action onStatusRemoved = null)
    {
        statusEffects.Remove(statusEffect);
        onStatusRemoved?.Invoke();
    }
}