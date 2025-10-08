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

    public GameObject projectilePrefab;

    private float fireCountdown = 0f;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    public TowerStatuses towerStatuses;

    void Start()
    {
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        towerStatuses = GetComponent<TowerStatuses>();
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
        towerStatuses.AddStatus(statuses, damage);

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
