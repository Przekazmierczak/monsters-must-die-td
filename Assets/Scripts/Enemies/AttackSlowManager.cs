using UnityEngine;

public class AttackSlowManager : SlowManagers
{
    public MaxHeap<Slow> slows = new MaxHeap<Slow>();

    public override bool Update()
    {
        float prevSpeed = finalSlow;
        while (slows.Count > 0 && slows.PeekMax().end <= Time.time)
        {
            slows.ExtractMax();
        }

        if (slows.Count > 0)
        {
            finalSlow = slows.PeekPriority();
        }
        else
        {
            finalSlow = 0f;
        }

        return prevSpeed == finalSlow;
    }
}
