using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public List<Vector3> waypoints;
    private int currentPoint = 0;

    public float speed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints.Add(new Vector3(8f, -3f, 0));
        waypoints.Add(new Vector3(14f, -3f, 0));
        waypoints.Add(new Vector3(14f, -8f, 0));
        waypoints.Add(new Vector3(8f, -8f, 0));
        waypoints.Add(new Vector3(8f, -11f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count == 0 || currentPoint >= waypoints.Count) return;

        Vector3 direction = (waypoints[currentPoint] - this.transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, waypoints[currentPoint]) < 0.1f)
        {
            currentPoint++;
        }
    }
}
