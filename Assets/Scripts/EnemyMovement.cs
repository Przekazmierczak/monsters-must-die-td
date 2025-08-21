using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // NavMeshAgent agent;
    private GameObject environment;
    private PathFinder pf;
    private int currentPoint = 0;

    void Awake()
    {
        environment = GameObject.Find("Environment");
        pf = environment.GetComponent<PathFinder>();
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

        if (Vector3.Distance(transform.position, pf.path.corners[currentPoint]) < 0.1f)
        {
            currentPoint++;
        }
    }
}
