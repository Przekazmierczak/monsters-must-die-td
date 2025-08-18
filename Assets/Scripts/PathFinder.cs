using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(8f, 0f, 0);
    private Vector3 endPosition = new Vector3(8f, -11f, 0);
    public NavMeshSurface surface;
    public NavMeshPath path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateNavMesh();
    }

    // Update is called once per frame
    public void UpdateNavMesh()
    {
        surface.BuildNavMesh();

        path = new NavMeshPath();
        NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path);

        Debug.Log(path.status);
    }
}
