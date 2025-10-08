public class Poison : Status
{
    public float power;
    
    public void Initialize(float statusDuration, float statusPower)
    {
        duration = statusDuration;
        power = statusPower;
    }

    public override void Apply(float statusEnd, Enemy statusTarget)
    {
        base.Apply(statusEnd, statusTarget);
        target.enemyStatuses.poisons.Insert(this, -statusEnd);
        target.enemyStatuses.poisonCumulation += power;
    }
}
