using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class CardPickerUI : MonoBehaviour
{
    public GameObject cardPickerPanel;
    [SerializeField] private CardPickerSlotManager[] slots;
    [SerializeField] private CardList cardList;
    [SerializeField] private InventoryUI inventoryUI;

    void Start()
    {
        cardPickerPanel.SetActive(false);
    }

    public void ShowCardPicker()
    {
        bool isActive = cardPickerPanel.activeSelf;
        cardPickerPanel.SetActive(!isActive);

        for (int i = 0; i < slots.Length; i++)
        {
            int index = Random.Range(0, cardList.allCards.Count - 1);
            slots[i].contain = cardList.allCards[index];
            slots[i].UpdateImage();
        }
    }

    public void OnCardSelected(Card card)
    {
        inventoryUI.AddCard(card);
        cardPickerPanel.SetActive(false);
    }
}