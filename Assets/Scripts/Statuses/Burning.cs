using UnityEngine;

public class Burning : Status
{
    public float power;
    public void Initialize(float statusDuration)
    {
        duration = statusDuration;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        if (target.burningPower <= power)
        {
            target.burningPower = power;
            target.burningEnd = Time.time + duration;
        }
    }

    public override void Remove()
    {
        // recalculate speed
    }
}
