using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TowersGrid : MonoBehaviour
{
    public GameObject environment;
    private PathFinder pf;

    public GameObject tower;
    public GameObject[,] grid = new GameObject[17, 12];

    void Awake()
    {
        pf = environment.GetComponent<PathFinder>();
    }

    public void AddTower(Vector2 position)
    {
        GameObject newTower = Instantiate(tower);
        newTower.transform.position = position;
        grid[(int)position.x, -(int)position.y] = newTower;

        // Start coroutine to update NavMesh next frame - collider not ready
        StartCoroutine(UpdateNavMeshNextFrame());
    }
    
    IEnumerator UpdateNavMeshNextFrame()
    {
    // Wait a few FixedUpdate frames for the collider to be fully registered
        for (int i = 0; i < 2; i++) yield return new WaitForFixedUpdate();

        pf.UpdateNavMesh();
    }
}
