using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;
    public Canvas canvas;
    public GameObject damagePopupPrefab;

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

    public void ShowDamage(Vector3 worldPosition, float damage)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition + Vector3.up * 0.1f + Vector3.right * 1f + new Vector3(Random.Range(-0.1f,0.1f),0,0));
        GameObject popup = Instantiate(damagePopupPrefab, screenPos, Quaternion.identity, canvas.transform);
        popup.GetComponent<DamagePopup>().Setup(damage);
    }
}
