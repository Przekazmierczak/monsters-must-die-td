using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TowersGrid : MonoBehaviour
{
    public GameObject environment;
    private PathFinder pf;

    public GameObject tower;
    // Vector2 position;
    List<Vector2> positions = new List<Vector2>();
    public GameObject[,] grid = new GameObject[17, 12];

    void Awake()
    {
        pf = environment.GetComponent<PathFinder>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positions.Add(new Vector2(5f, -1f));
        positions.Add(new Vector2(6f, -1f));
        positions.Add(new Vector2(7f, -1f));
        positions.Add(new Vector2(8f, -1f));
        positions.Add(new Vector2(9f, -1f));
        positions.Add(new Vector2(9f, -2f));
        positions.Add(new Vector2(10f, -2f));
        positions.Add(new Vector2(11f, -2f));
        positions.Add(new Vector2(12f, -2f));

        foreach (Vector2 position in positions)
        {
            AddTower(position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddTower(Vector2 position)
    {
        GameObject newTower = Instantiate(tower);
        newTower.transform.position = position;
        grid[(int)position.x, -(int)position.y] = newTower;

        // Start coroutine to update NavMesh next frame - collider not ready
        StartCoroutine(UpdateNavMeshNextFrame());
    }
    
    IEnumerator UpdateNavMeshNextFrame()
{
    yield return null; // wait 1 frame for physics update
    pf.UpdateNavMesh();
}
}
