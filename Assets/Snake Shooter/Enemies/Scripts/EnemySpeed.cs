using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float baseValue = 1.0f;
    [SerializeField] private float growth = 0.05f;


    private float value;
    public float Value
    {
        get
        {
            var retVal = value;
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

    private void Awake()
    {
        value = BaseValue;
        LevelManager.OnRoundBegun += UpdateStat;
    }

    private void OnDestroy()
    {
        LevelManager.OnRoundBegun -= UpdateStat;
    }

    private void UpdateStat(OnRoundBegunEventArgs args)
    {
        Value = BaseValue + Growth * (args.roundIndex) + Random.Range(-0.1f, 0.1f);
    }
}
