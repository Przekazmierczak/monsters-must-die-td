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
        if (Time.time > target.enemyStatuses.frozen)
        {
            target.enemyStatuses.chillSlowManager.chills.Insert(this, Time.time + duration);
            target.enemyStatuses.chillSlowManager.chillCumulation += power;

            if (target.enemyStatuses.chillSlowManager.chillCumulation >= 0.5 * target.maxHealth)
            {
                target.enemyStatuses.frozen = Time.time + freezDuration;
                target.enemyStatuses.chillSlowManager.chills = new MaxHeap<ChillFreeze>();
                target.enemyStatuses.chillSlowManager.chillCumulation = 0f;
            }
        }
    }
}
