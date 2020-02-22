using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float baseValue = 1.0f;
    [SerializeField] private float growth = 0.05f;

    private List<StatModifier> statModifiers;


    private float value;
    public float Value
    {
        get
        {
            var retVal = value;
            statModifiers.ForEach((statModifier) =>
            {
                retVal = statModifier.Modify(retVal);
            });
            return retVal;
        }
        set
        {
            if (value <= 0) this.value = 0;
            else this.value = value;
        }
    }

    public float BaseValue => baseValue;
    public float Growth => growth;

    private void Start()
    {
        value = BaseValue;
    }

    private void OnEnable()
    {
        statModifiers = new List<StatModifier>();
    }

    private void OnDisable()
    {
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier statModifier)
    {
        statModifiers.Add(statModifier);
        StartCoroutine(ModifyUpdate(statModifier));
    }

    public IEnumerator ModifyUpdate(StatModifier statModifier)
    {
        yield return new WaitForSeconds(statModifier.Duration);
        RemoveModifier(statModifier);
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        statModifiers.Remove(statModifier);
    }
}
