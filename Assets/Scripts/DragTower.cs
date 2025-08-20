using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTower : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // [SerializeField] private Canvas canvas;
    public GameObject TowerIndicator;

    // private RectTransform rectTransform;

    // private void Awake()
    // {
    //     rectTransform = GetComponent<RectTransform>();
    // }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TowerIndicator.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        TowerIndicator.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TowerIndicator.SetActive(false);
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     Debug.Log("Clicked");
    // }
}
