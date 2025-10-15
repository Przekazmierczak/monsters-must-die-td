using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTower : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // [SerializeField] private Canvas canvas;
    public GameObject TowerIndicator;
    public SquareIndicator squareIndicator;
    public GameObject SquareIndicat;

    public void OnBeginDrag(PointerEventData eventData)
    {
        TowerIndicator.SetActive(true);
        squareIndicator.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        TowerIndicator.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TowerIndicator.SetActive(false);
        SquareIndicat.SetActive(false);
        squareIndicator.enabled = false;
    }
}
