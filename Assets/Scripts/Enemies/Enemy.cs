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
    public MaxHeap<Poison> poisons;
    public float poisonCumulation = 0f;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Move(baseSpeed);
        statuses = new List<Status>();
        slows = new MaxHeap<Slow>();
        poisons = new MaxHeap<Poison>();
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
    public void TakePoisonDamage()
    {
        health -= poisonCumulation * Time.deltaTime;
        if (health <= 0)
        {
            Die();
        }
    }

    public void ShowPoisonDamage()
    {
        GameManager.Instance.ShowDamage(transform.position, poisonCumulation, new Color(0.6f, 0f, 0.6f, 1f));
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

        while (poisons.Count > 0 && poisons.PeekMax().end < Time.time)
        {
            Poison currentPoison = poisons.ExtractMax();
            poisonCumulation -= currentPoison.power;
        }

        if (poisons.Count > 0)
        {
            if (GameManager.Instance.showPoisonDamage == true) { ShowPoisonDamage(); };
            TakePoisonDamage();
        }
    }
}