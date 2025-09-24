using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

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
        if (grid[(int)position.x, -(int)position.y] == null)
        {
            GameObject newTower = Instantiate(tower);
            grid[(int)position.x, -(int)position.y] = newTower;
            newTower.transform.position = position;

            // Start coroutine to update NavMesh next frame - collider not ready
            StartCoroutine(UpdateNavMeshNextFrame(newTower));
        }
    }

    IEnumerator UpdateNavMeshNextFrame(GameObject newTower)
    {
        // Wait a few FixedUpdate frames for the collider to be fully registered
        for (int i = 0; i < 2; i++) yield return new WaitForFixedUpdate();

        bool blocking = pf.UpdateNavMesh();
        if (blocking)
        {
            Destroy(newTower);
        }
    }
}
