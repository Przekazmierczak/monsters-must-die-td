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
    public MaxHeap<Slow> slows;
    public float slow = 0f;
    public MaxHeap<Poison> poisons;
    public float poisonCumulation = 0f;
    public float gustEnd = 0f;
    bool collide = false;
    public float burningPower = 0f;
    public float burningEnd = 0f;
    public MaxHeap<ChillFreeze> chills;
    public float chillCumulation = 0f;
    float chillSlow = 0f;
    public float frozen = 0f;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Move(baseSpeed);
        slows = new MaxHeap<Slow>();
        poisons = new MaxHeap<Poison>();
        chills = new MaxHeap<ChillFreeze>();
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

    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (slows.Count > 0)
        {
            while (slows.Count > 0 && slows.PeekMax().end <= Time.time)
            {
                slows.ExtractMax();
            }

            if (slows.Count > 0)
            {
                slow = slows.PeekPriority();
            }
            else
            {
                slow = 0f;
            }
        }

        while (chills.Count > 0 && chills.PeekMax().end <= Time.time)
        {
            ChillFreeze currentChill = chills.ExtractMax();
            chillCumulation -= currentChill.power;
        }

        if (chills.Count > 0)
        {
            chillSlow = Math.Max(chillCumulation / (maxHealth * 0.3f), 1) * GameManager.Instance.maxChillPotency;
        }
        else
        {
            chillSlow = 0f;
        }

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
            currentSpeed = baseSpeed;
            currentSpeed -= currentSpeed * slow;
            currentSpeed -= currentSpeed * chillSlow;
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