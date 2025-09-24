using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private LayerMask clickableMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, clickableMask);

            if (hit.collider != null)
            {
                ClickedObject clicked = hit.collider.GetComponent<ClickedObject>();
                
                if (clicked != null)
                {
                    clicked.OnClick();
                }
            }
        }
    }
}