using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // NavMeshAgent agent;
    public GameObject environment;
    private PathFinder pf;
    private int currentPoint = 0;
    public float speed = 5f;

    void Awake()
    {
        pf = environment.GetComponent<PathFinder>();
    }

    // Update is called once per frame
    void Update()
    {
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
