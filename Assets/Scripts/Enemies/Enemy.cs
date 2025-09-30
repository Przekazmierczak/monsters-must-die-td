using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public Canvas canvas;

    public float health = 10;
    public float maxHealth = 10;
    public float baseSpeed = 2f;
    public float currentSpeed = 2f;
    EnemyMovement movement;
    // public float stunTime;
    public List<Status> statuses;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Move(baseSpeed);
        statuses = new List<Status>();
    }

    public void TakeDamage(float damage)
    {
        GameManager.Instance.ShowDamage(transform.position, damage);

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        for (int i = statuses.Count - 1; i >= 0; i--)
        {
            if (Time.time >= statuses[i].end)
            {
                statuses[i].Remove();
                statuses.RemoveAt(i);
            }
        }
        movement.Move(currentSpeed);
    }
}