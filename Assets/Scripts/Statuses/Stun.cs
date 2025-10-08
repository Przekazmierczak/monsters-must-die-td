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
        if (target.enemyStatuses.stun < Time.time + duration)
        {
            target.enemyStatuses.stun = Time.time + duration;
        }
    }
}
