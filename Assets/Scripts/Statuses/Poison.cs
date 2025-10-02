using UnityEngine;

public class Poison : Status
{
    public float power;
    public void Initialize(float statusDuration)
    {
        duration = statusDuration;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        target.poisons.Insert(this, Time.time + duration);
        target.poisonCumulation += power;
    }

    public override void Remove()
    {
        // recalculate speed
    }
}
