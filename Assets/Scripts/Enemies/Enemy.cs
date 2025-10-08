using System;
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

    public EnemyStatuses enemyStatuses;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Move(baseSpeed);
        enemyStatuses = GetComponent<EnemyStatuses>();
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

    public void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        float speed = enemyStatuses.ManageStatuses();
        movement.Move(speed);
    }
}