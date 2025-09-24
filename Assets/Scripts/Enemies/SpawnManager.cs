using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    // private Coroutine spawningEnemy;

    public void SpawnWave()
    {
        StartCoroutine(SpawningEnemy(10));
    }

    IEnumerator SpawningEnemy(int numberOfEnemy)
    {
        while (numberOfEnemy > 0)
        {
            Instantiate(enemy, new Vector3(6f, 1f, 0), Quaternion.identity);
            numberOfEnemy--;
            yield return new WaitForSeconds(1f);
        }
    }
}
