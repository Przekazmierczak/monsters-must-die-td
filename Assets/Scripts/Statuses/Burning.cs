using UnityEngine;

public class Burning : Status
{
    public float power;
    
    public void Initialize(float statusDuration, float statusPower)
    {
        duration = statusDuration;
        power = statusPower;
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
}
