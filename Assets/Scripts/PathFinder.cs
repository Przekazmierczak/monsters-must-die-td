using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(6f, 1f, 0);
    private Vector3 endPosition = new Vector3(6f, -9f, 0);
    public NavMeshSurface surface;
    public NavMeshPath path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateNavMesh();
    }

    public void UpdateNavMesh()
    {
        surface.BuildNavMesh();

        path = new NavMeshPath();
        NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path);
    }
}
