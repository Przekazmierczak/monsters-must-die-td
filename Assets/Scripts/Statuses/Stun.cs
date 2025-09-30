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
        target.statuses.Add(this);
        target.currentSpeed = 0f;
    }

    // Update is called once per frame
    public override void Remove()
    {
        target.currentSpeed = target.baseSpeed;
    }
}
