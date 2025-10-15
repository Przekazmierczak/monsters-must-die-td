using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    private GameObject floor;
    private LayerMask mask;
    private Collider2D floorCollider;

    private GameObject platform;
    private TowersGrid tg;

    private void Awake()
    {
        floor = GameObject.Find("Floor");
        mask = LayerMask.GetMask("Floor");
        floorCollider = floor.GetComponent<Collider2D>();

        platform = GameObject.Find("Platform");
        tg = platform.GetComponent<TowersGrid>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Collider2D hit = Physics2D.OverlapPoint(worldPos, mask);

        if (hit == floorCollider)
        {
            tg.AddTower(new Vector2(Mathf.Round(worldPos.x), Mathf.Round(worldPos.y)));
        }
    }
}
