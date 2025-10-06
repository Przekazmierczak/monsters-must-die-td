using UnityEngine;

public class ProjectileStatus
{
    public Status status;
    public float chance;

    public void Initialize(Status projectileStatus, float projectileStatusChance)
    {
        status = projectileStatus;
        chance = projectileStatusChance;
    }

    public void Hit(Enemy target)
    {
        if (chance == 1)
        {
            status.Apply(Time.time + status.duration, target);
        }
        else if (UnityEngine.Random.Range(0f, 1f) <= chance)
        {
            status.Apply(Time.time + status.duration, target);
        }
    }
}
