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
        target.gustEnd = Math.Max(target.gustEnd, statusEnd);
    }

    public override void Remove()
    {
    }
}
