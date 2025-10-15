using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    public Card contain = null;

    // private Button button;

    // public CardPickerUI cardPickerUI;
    private Image cardImage;
    public Sprite defaultSlotImage;

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

    public void UpdateImage(bool ifDrag)
    {
        if (ifDrag == false)
        {
            cardImage.sprite = contain.icon;
        }
        else
        {
            cardImage.sprite = defaultSlotImage;
        }
    }
}
