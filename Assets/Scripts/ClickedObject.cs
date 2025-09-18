using UnityEngine;

public class ClickedObject : MonoBehaviour
{
    private GameObject highlightObj;

    public void Highlight()
    {
        if (highlightObj != null) return; // already highlighted

        // Create a child object
        highlightObj = new GameObject("Highlight");
        highlightObj.transform.SetParent(transform, false);
        highlightObj.transform.localPosition = Vector3.zero;

        // Copy sprite renderer
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        SpriteRenderer highlightSR = highlightObj.AddComponent<SpriteRenderer>();
        highlightSR.sprite = sr.sprite;
        highlightSR.color = Color.red;

        // Make it slightly bigger and behind
        highlightObj.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        highlightSR.sortingOrder = sr.sortingOrder - 1;
    }

    public void RemoveHighlight()
    {
        if (highlightObj != null)
        {
            Destroy(highlightObj);
            highlightObj = null;
            Debug.Log("test");
        }
    }

    public void OnClick()
    {
        if (GameManager.Instance.currentMarked != null)
        {
            ClickedObject prev = GameManager.Instance.currentMarked.GetComponent<ClickedObject>();
            prev.RemoveHighlight();
        }

        GameManager.Instance.MarkObject(gameObject);
        Highlight();
    }
}
