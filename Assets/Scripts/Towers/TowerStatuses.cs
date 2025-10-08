using System.Collections.Generic;
using UnityEngine;

public class TowerStatuses : MonoBehaviour
{
    public float stunChance = 0f;
    public float stunDuration = 0f;
    public float slowPower = 0f;
    public float slowDuration = 0f;
    public float poisonPower = 0f;
    public float poisonDuration = 0f;
    public float gustChance = 0f;
    public float gustDuration = 0f;
    public float burningPower = 0f;
    public float burningDuration = 0f;
    public bool frost = false;
    public float frostDuration = 0f;
    public float freezDuration = 0f;

    public void AddStatus(List<ProjectileStatus> statuses, float damage)
    {
        if (stunChance != 0 && stunDuration != 0)
        {
            Stun stun = new Stun();
            stun.Initialize(stunDuration);

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.Initialize(stun, stunChance);

            statuses.Add(projectileStatus);
        }

        if (slowPower != 0 && slowDuration != 0)
        {
            Slow slow = new Slow();
            slow.Initialize(slowDuration, slowPower);

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.Initialize(slow, 1f);

            statuses.Add(projectileStatus);
        }

        if (poisonPower != 0 && poisonDuration != 0)
        {
            Poison poison = new Poison();
            poison.Initialize(poisonDuration, poisonPower);

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.Initialize(poison, 1f);

            statuses.Add(projectileStatus);
        }

        if (gustChance != 0 && gustDuration != 0)
        {
            Gust gust = new Gust();
            gust.Initialize(gustDuration);

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.Initialize(gust, gustChance);

            statuses.Add(projectileStatus);
        }

        if (burningPower != 0f && burningDuration != 0f)
        {
            Burning burning = new Burning();
            burning.Initialize(burningDuration, burningPower * damage);

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.Initialize(burning, 1f);

            statuses.Add(projectileStatus);
        }

        if (frost == true)
        {
            ChillFreeze chillFreeze = new ChillFreeze();
            chillFreeze.Initialize(frostDuration, damage, freezDuration);

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.Initialize(chillFreeze, 1f);

            statuses.Add(projectileStatus);
        }
    }
}
