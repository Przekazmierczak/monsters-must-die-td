using UnityEngine;

public class ClickedObject : MonoBehaviour
{

    public Material outline;
    public Material spriteLitDefault;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void Highlight()
    {
        rend.material = outline;
    }

    public void RemoveHighlight()
    {
        rend.material = spriteLitDefault;
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
