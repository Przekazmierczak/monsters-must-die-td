using System;
using UnityEngine;

public class Gust : Status
{
    public void Initialize(float statusDuration)
    {
        duration = statusDuration;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        target.enemyStatuses.gustEnd = Math.Max(target.enemyStatuses.gustEnd, statusEnd);
    }
}
