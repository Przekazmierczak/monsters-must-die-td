using UnityEngine;

public class ChillFreeze : Status
{
    public float power;
    public float freezDuration = 0f;
    
    public void Initialize(float statusDuration, float statusPower, float statusFreezDuration)
    {
        duration = statusDuration;
        power = statusPower;
        freezDuration = statusFreezDuration;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        if (Time.time > target.frozen)
        {
            target.chills.Insert(this, Time.time + duration);
            target.chillCumulation += power;

            if (target.chillCumulation >= 0.5 * target.maxHealth)
            {
                target.frozen = Time.time + freezDuration;
                target.chills = new MaxHeap<ChillFreeze>();
                target.chillCumulation = 0f;
            }
        }
    }
}
