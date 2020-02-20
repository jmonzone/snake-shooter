using UnityEngine;

public class BurnEffect : StatusEffect
{
    private readonly float burnValue;

    public BurnEffect(float burnValue, float duration) : base(duration)
    {
        this.burnValue = burnValue;
    }

    public override void Afflict(Enemy enemy)
    {
        base.Afflict(enemy);
        enemy.Health.AddOvertimeEffect(() =>
        {
            enemy.Health.Value -= burnValue;

        }, Duration);
    }
}
