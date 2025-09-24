using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(6f, 1f, 0);
    private Vector3 endPosition = new Vector3(6f, -9f, 0);
    public NavMeshSurface surface;
    public NavMeshPath path;
    public NavMeshPath testPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateNavMesh();
    }

    public bool UpdateNavMesh()
    {
        surface.BuildNavMesh();

        testPath = new NavMeshPath();
        NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, testPath);

        if (testPath.status == NavMeshPathStatus.PathComplete)
        {
            path = testPath;
            return false;
        }
        else
        {
            return true;
        }
    }
}
