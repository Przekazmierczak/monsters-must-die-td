using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Global data
    public int level = 1;
    public int gold = 10;

    // Global marked object
    public GameObject currentMarked;

    void Awake()
    {
        // Make sure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkObject(GameObject obj)
    {
        currentMarked = obj;
    }
}
