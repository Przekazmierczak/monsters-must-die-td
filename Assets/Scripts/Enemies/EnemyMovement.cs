using System.Data.Common;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // NavMeshAgent agent;
    private GameObject environment;
    private PathFinder pf;
    private SpriteRenderer sr;
    private int currentPoint = 0;
    bool isFlipped = false;

    void Awake()
    {
        environment = GameObject.Find("Environment");
        pf = environment.GetComponent<PathFinder>();
        sr = GetEnemySprite();
    }

    // Update is called once per frame
    public void Move(float speed)
    {
        if (currentPoint >= pf.path.corners.Length)
        {
            Destroy(gameObject);
        }

        if (currentPoint >= pf.path.corners.Length || pf.path == null) return;

        // Move toward current corner
        Vector3 direction = (pf.path.corners[currentPoint] - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 95f)
        {
            isFlipped = true;
            sr.flipX = isFlipped;
        }
        else if (Mathf.Abs(angle) < 85f)
        {
            isFlipped = false;
            sr.flipX = isFlipped;
        }
        else
        {
            sr.flipX = !isFlipped;
        }

        if (Vector3.Distance(transform.position, pf.path.corners[currentPoint]) < 0.1f)
        {
            currentPoint++;
        }
    }
    
    SpriteRenderer GetEnemySprite()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemySprite"))
            {
                return child.GetComponent<SpriteRenderer>();
            }
        }
        return null;
    }
}
