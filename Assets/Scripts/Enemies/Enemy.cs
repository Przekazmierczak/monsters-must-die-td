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
    public float stun = 0f;
    public MaxHeap<Poison> poisons;
    public float poisonCumulation = 0f;
    public float gustEnd = 0f;
    bool collide = false;
    public float burningPower = 0f;
    public float burningEnd = 0f;

    public float frozen = 0f;

    public List<SlowManagers> slowManagers;

    public AttackSlowManager attackSlowManager;
    public ChillSlowManager chillSlowManager;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Move(baseSpeed);
        poisons = new MaxHeap<Poison>();

        slowManagers = new List<SlowManagers>();
        attackSlowManager = new AttackSlowManager();
        chillSlowManager = new ChillSlowManager();
        chillSlowManager.chillThreshold = maxHealth * 0.3f;
        slowManagers.Add(attackSlowManager);
        slowManagers.Add(chillSlowManager);
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
    
    public void TakeBurningDamage()
    {
        health -= burningPower;
        if (health <= 0)
        {
            Die();
        }
    }

    public void ShowBurningDamage()
    {
        GameManager.Instance.ShowDamage(transform.position, burningPower, new Color(1f, 0.3f, 0f, 1f));
    }

    public void UpdateMovementSpeed()
    {
        currentSpeed = baseSpeed;
        foreach (var slowManager in slowManagers)
        {
            currentSpeed -= currentSpeed * slowManager.finalSlow;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (Time.time < gustEnd)
        {
            if (collide == false)
            {
                movement.Move(-1f);
            }
            else
            {
                gustEnd = Time.time;
            }
        }
        else if (Time.time < frozen || Time.time < stun)
        {
            movement.Move(0f);
        }
        else
        {
            if (attackSlowManager.Update() == true ||
                chillSlowManager.Update() == true)
            {
                UpdateMovementSpeed();
            }
            movement.Move(currentSpeed);
        }

        while (poisons.Count > 0 && poisons.PeekMax().end <= Time.time)
        {
            Poison currentPoison = poisons.ExtractMax();
            poisonCumulation -= currentPoison.power;
        }

        if (poisons.Count > 0)
        {
            if (GameManager.Instance.showPoisonDamage == true) { ShowPoisonDamage(); }
            ;
            TakePoisonDamage();
        }

        if (burningPower > 0f && Time.time <= burningEnd && GameManager.Instance.dealShowBurningDamage == true)
        {
            TakeBurningDamage();
            ShowBurningDamage();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PushCollider"))
        {
            collide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PushCollider"))
        {
            collide = false;
        }
    }
}