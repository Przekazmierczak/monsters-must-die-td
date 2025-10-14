using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    public Card contain = null;

    // private Button button;

    // public CardPickerUI cardPickerUI;
    private Image cardImage;

    void Awake()
    {
        // button = GetComponent<Button>();
        cardImage = GetComponent<Image>();
        // button.onClick.AddListener(() => onPick());
    }

    // void onPick()
    // {
    //     cardPickerUI.OnCardSelected(contain);
    // }

    public void UpdateImage()
    {
        cardImage.sprite = contain.icon;
    }
}
