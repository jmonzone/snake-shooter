using System;

[Serializable]
public abstract class StatModifier
{
    protected float modifier;
    protected float duration;
    public float Duration => duration;

    public StatModifier(float modifier, float duration)
    {
        this.modifier = modifier;
        this.duration = duration;
    }

    public abstract float Modify(float value);
}

public class AdditiveModifier : StatModifier
{
    public AdditiveModifier(float modifier, float duration) : base(modifier, duration) { }

    public override float Modify(float value)
    {
        return value + modifier;
    }

}

public class MultiplicativeModifier : StatModifier
{
    public MultiplicativeModifier(float modifier, float duration) : base(modifier, duration) { }

    public override float Modify(float value)
    {
        return value * modifier;
    }
}