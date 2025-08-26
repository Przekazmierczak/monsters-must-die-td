using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    public float speed = 10f;
    public float destroyDistance = 0.2f;
    public float damage;

    public void Seek(Enemy trackTarget, float projectileDamage)
    {
        target = trackTarget;
        damage = projectileDamage;
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
            target.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Rotate if moving
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
