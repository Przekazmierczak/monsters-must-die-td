public class Slow : Status
{
    public float slowPower;
    public void Initialize(float statusDuration)
    {
        duration = statusDuration;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        target.slows.Insert(this, slowPower);
    }

    public override void Remove()
    {
        // recalculate speed
    }
}
