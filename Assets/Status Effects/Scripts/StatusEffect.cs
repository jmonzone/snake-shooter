public class StatusEffect
{
    public float Duration { get; }

    public StatusEffect(float duration)
    {
        Duration = duration;
    }

    public virtual void Afflict(Enemy enemy) { }
}
