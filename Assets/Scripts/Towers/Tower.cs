using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class Tower : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float damage = 5;
    public float range;
    public int multiShot = 1;
    public float area = 0;
    public int chain = 0;
    public float stunChance = 0f;
    public float stunDuration = 0f;
    public float slowPower = 0f;
    public float slowDuration = 0f;
    public float poisonPower = 0f;
    public float poisonDuration = 0f;
    public float gustChance = 0f;
    public float gustDuration = 0f;

    public GameObject projectilePrefab;
    // public Transform firePoint;

    private float fireCountdown = 0f;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    void Start()
    {
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        range = circleCollider.radius;
    }

    void Update()
    {
        // Remove nulls in case enemies die
        enemiesInRange.RemoveAll(e => e == null);

        if (enemiesInRange.Count > 0)
        {
            fireCountdown -= Time.deltaTime;
            if (fireCountdown <= 0f)
            {
                for (int i = 0; i < Math.Min(multiShot, enemiesInRange.Count); i++)
                {
                    Shoot(enemiesInRange[i]);
                }
                fireCountdown = 1f / attackSpeed;
            }
        }
    }

    void Shoot(Enemy target)
    {
        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        List<Collider2D> hitEnemies = new List<Collider2D> {target.GetComponent<Collider2D>()};

        List<ProjectileStatus> statuses = new List<ProjectileStatus>();

        if (stunChance != 0 && stunDuration != 0)
        {
            Stun stun = new Stun();
            stun.duration = stunDuration;

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.status = stun;
            projectileStatus.chance = stunChance;

            statuses.Add(projectileStatus);
        }

        if (slowPower != 0 && slowDuration != 0)
        {
            Slow slow = new Slow();
            slow.duration = slowDuration;
            slow.power = slowPower;

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.status = slow;
            projectileStatus.chance = 1f;

            statuses.Add(projectileStatus);
        }

        if (poisonPower != 0 && poisonDuration != 0)
        {
            Poison poison = new Poison();
            poison.duration = poisonDuration;
            poison.power = poisonPower;

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.status = poison;
            projectileStatus.chance = 1f;

            statuses.Add(projectileStatus);
        }

        if (gustChance != 0 && gustDuration != 0)
        {
            Gust gust = new Gust();
            gust.duration = gustDuration;

            ProjectileStatus projectileStatus = new ProjectileStatus();
            projectileStatus.status = gust;
            projectileStatus.chance = gustChance;

            statuses.Add(projectileStatus);
        }

        if (projectile != null)
        {
            projectile.Seek(target, projectilePrefab, damage, area, chain, hitEnemies, statuses);
        }
    }

    // Detect enemies entering range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
