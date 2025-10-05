using UnityEngine;

public class Stun : Status
{
    public void Initialize(float statusDuration)
    {
        duration = statusDuration;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        // target.statuses.Add(this);
        if (target.stun < Time.time + duration)
        {
            target.stun = Time.time + duration;
        }
    }

    public override void Remove()
    {
        target.currentSpeed = target.baseSpeed;
    }
}
