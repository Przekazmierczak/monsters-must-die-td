using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    public GameObject prefab;
    public float speed = 10f;
    public float destroyDistance = 0.2f;
    public float damage;
    public float area;
    public int chain;
    List<Collider2D> hitEnemies;
    List<ProjectileStatus> projectileStatuses;


    public void Seek(Enemy trackTarget, GameObject projectilePrefab, float projectileDamage, float projectileArea, int projectileChain, List<Collider2D> projectileHitEnemies, List<ProjectileStatus> projectileProjectileStatuses)
    {
        target = trackTarget;
        prefab = projectilePrefab;
        damage = projectileDamage;
        area = projectileArea;
        chain = projectileChain;
        hitEnemies = projectileHitEnemies;
        projectileStatuses = projectileProjectileStatuses;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward target
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if close enough to "hit"
        if (Vector3.Distance(transform.position, target.transform.position) < destroyDistance)
        {
            int enemyLayer = LayerMask.GetMask("Enemy");
            if (chain > 0)
            {
                GameObject projectileGO = Instantiate(prefab, transform.position, transform.rotation);
                Projectile projectile = projectileGO.GetComponent<Projectile>();

                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5, enemyLayer);

                Enemy newTarget = null;
                float minDistance = Mathf.Infinity;

                foreach (var currHit in hits)
                {
                    if (hitEnemies.Contains(currHit)) continue;

                    Enemy currTarget = currHit.GetComponent<Enemy>();
                    if (currTarget == null) continue;

                    float dist = Vector2.Distance(transform.position, currHit.transform.position);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        newTarget = currTarget;
                    }
                }
                if (newTarget != null)
                {
                    hitEnemies.Add(newTarget.GetComponent<Collider2D>());
                    projectile.Seek(newTarget, prefab, damage, area, chain - 1, hitEnemies, projectileStatuses);
                }
            }
            if (area == 0)
            {
                if (damage > 0) { target.TakeDamage(damage); }
                foreach (var status in projectileStatuses)
                {
                    status.Hit(target);
                }

                Destroy(gameObject);
                return;
            }
            else
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, area, enemyLayer);

                foreach (var currHit in hits)
                {
                    Enemy currTarget = currHit.GetComponent<Enemy>();
                    if (damage > 0) { currTarget.TakeDamage(damage); }

                    foreach (var status in projectileStatuses)
                    {
                        status.Hit(currTarget);
                    }
                }
                Destroy(gameObject);
                return;
            }
            
        }

        // Rotate if moving
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
