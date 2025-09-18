using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public float fireRate = 1f;
    public float damage = 5;
    public GameObject projectilePrefab;
    // public Transform firePoint;

    private float fireCountdown = 0f;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    void Update()
    {
        // Remove nulls in case enemies die
        enemiesInRange.RemoveAll(e => e == null);

        if (enemiesInRange.Count > 0)
        {
            fireCountdown -= Time.deltaTime;
            if (fireCountdown <= 0f)
            {
                Shoot(enemiesInRange[0]); // shoot first enemy
                fireCountdown = 1f / fireRate;
            }
        }
    }

    void Shoot(Enemy target)
    {
        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Seek(target, damage);
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
