using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject CardIndicator;
    public SquareIndicator squareIndicator;
    public GameObject SquareIndicat;
    public Image cardIndicatorImage;
    private InventorySlotManager inventorySlotManager;

    void Awake()
    {
        inventorySlotManager = GetComponent<InventorySlotManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventorySlotManager.contain == null) return;

        CardIndicator.SetActive(true);
        inventorySlotManager.UpdateImage(true);
        cardIndicatorImage.sprite = inventorySlotManager.contain.icon;
        squareIndicator.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventorySlotManager.contain == null) return;

        CardIndicator.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventorySlotManager.contain == null) return;
        
        CardIndicator.SetActive(false);
        SquareIndicat.SetActive(false);
        squareIndicator.enabled = false;
        inventorySlotManager.UpdateImage(false);
    }
}
