using UnityEngine;
using UnityEngine.UI;

public class CardPickerUI : MonoBehaviour
{
    public GameObject cardPickerPanel;
    public Button pickCardButton;

    void Start()
    {
        cardPickerPanel.SetActive(false);

        pickCardButton.onClick.AddListener(ShowCardPicker);
    }

    void ShowCardPicker()
    {
        bool isActive = cardPickerPanel.activeSelf;
        cardPickerPanel.SetActive(!isActive);
    }

    public void OnCardSelected(string cardName)
    {
        Debug.Log("You picked: " + cardName);
        cardPickerPanel.SetActive(false);
    }
}