using UnityEngine;
using System.Collections.Generic;

public class EnemyStatuses : MonoBehaviour
{
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

    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();

        poisons = new MaxHeap<Poison>();
        slowManagers = new List<SlowManagers>();
        attackSlowManager = new AttackSlowManager();
        chillSlowManager = new ChillSlowManager();
        chillSlowManager.chillThreshold = enemy.maxHealth * 0.3f;
        slowManagers.Add(attackSlowManager);
        slowManagers.Add(chillSlowManager);
    }

    public void TakePoisonDamage()
    {
        enemy.health -= poisonCumulation * Time.deltaTime;
        if (enemy.health <= 0)
        {
            enemy.Die();
        }
    }

    public void ShowPoisonDamage()
    {
        GameManager.Instance.ShowDamage(transform.position, poisonCumulation, new Color(0.6f, 0f, 0.6f, 1f));
    }
    
    public void TakeBurningDamage()
    {
        enemy.health -= burningPower;
        if (enemy.health <= 0)
        {
            enemy.Die();
        }
    }

    public void ShowBurningDamage()
    {
        GameManager.Instance.ShowDamage(transform.position, burningPower, new Color(1f, 0.3f, 0f, 1f));
    }

    public void UpdateMovementSpeed()
    {
        enemy.currentSpeed = enemy.baseSpeed;
        foreach (var slowManager in slowManagers)
        {
            enemy.currentSpeed -= enemy.currentSpeed * slowManager.finalSlow;
        }
    }

    public float ManageStatuses()
    {
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

        if (Time.time < gustEnd)
        {
            if (collide == false)
            {
                return -1f;
            }
            else
            {
                gustEnd = Time.time;
                return 0f;
            }
        }
        else if (Time.time < frozen || Time.time < stun)
        {
            return 0f;
        }
        else
        {
            if (attackSlowManager.Update() == true ||
                chillSlowManager.Update() == true)
            {
                UpdateMovementSpeed();
            }
            return enemy.currentSpeed;
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
