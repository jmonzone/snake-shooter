using UnityEngine;

public class FreezeEffect : StatusEffect
{
    private readonly float slowValue;

    public FreezeEffect(float slowValue, float duration) : base(duration)
    {
        this.slowValue = slowValue;
    }

    public override void Afflict(Enemy enemy)
    {
        base.Afflict(enemy);

        var slow = new MultiplicativeModifier(slowValue, Duration);
        enemy.Speed.AddModifier(slow);
    }
}
