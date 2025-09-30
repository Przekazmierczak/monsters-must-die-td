using UnityEngine;

public class ProjectileStatus
{
    public Status status;
    public float chance;

    public void Hit(Enemy target)
    {
        if (UnityEngine.Random.Range(0f, 1f) <= chance)
        {
            status.Apply(Time.time + status.duration, target);
        }
    }
}
