using System;
using UnityEngine;

public class ChillSlowManager : SlowManagers
{
    public float chillCumulation = 0f;
    public float chillThreshold = 0f;
    public MaxHeap<ChillFreeze> chills = new MaxHeap<ChillFreeze>();

    public override bool Update()
    {
        float prevSpeed = finalSlow;
        while (chills.Count > 0 && chills.PeekMax().end <= Time.time)
        {
            ChillFreeze currentChill = chills.ExtractMax();
            chillCumulation -= currentChill.power;
        }

        if (chills.Count > 0)
        {
            finalSlow = Math.Max(chillCumulation / chillThreshold, 1) * GameManager.Instance.maxChillPotency;
        }
        else
        {
            finalSlow = 0f;
        }

        return prevSpeed == finalSlow;
    }
}
