using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlotManager[] slots;


    public int AddCard(Card card)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].contain == null)
            {
                slots[i].contain = card;
                slots[i].UpdateImage();
                return 0;
            }
        }
        return 1;
    }
}
