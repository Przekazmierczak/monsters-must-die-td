using UnityEngine;

public abstract class Status
{
    public float duration;
    public float end;
    public Enemy target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Apply(float statusEnd, Enemy statusTarget)
    {
        end = statusEnd;
        target = statusTarget;
    }

    public virtual void Remove() {}
}
