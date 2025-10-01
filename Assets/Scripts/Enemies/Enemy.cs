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
    public MaxHeap<Slow> slows;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Move(baseSpeed);
        statuses = new List<Status>();
        slows = new MaxHeap<Slow>();
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

        if (slows.Count > 0)
        {
            while (slows.Count > 0 && slows.PeekMax().end < Time.time)
            {
                slows.ExtractMax();
            }

            if (slows.Count > 0)
            {
                currentSpeed = baseSpeed - baseSpeed * slows.PeekPriority();
            }
            else
            {
                currentSpeed = baseSpeed;
            }
        }
        movement.Move(currentSpeed);
    }
}