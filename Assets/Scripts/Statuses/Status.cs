public abstract class Status
{
    public float duration;
    public float end;
    public Enemy target;

    public virtual void Apply(float statusEnd, Enemy statusTarget)
    {
        end = statusEnd;
        target = statusTarget;
    }
}
