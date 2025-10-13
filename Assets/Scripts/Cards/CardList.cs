using UnityEngine;
using System.Collections.Generic;

public class CardList : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    void Awake()
    {
        Card[] tempCards = Resources.LoadAll<Card>("");

        foreach (var card in tempCards)
        {
            int weight = GetWeight(card.rarity);

            // Add the item multiple times according to its weight
            for (int i = 0; i < weight; i++)
                allCards.Add(card);
        }

        Debug.Log($"Loaded {allCards.Count} cards.");
    }

    public int GetWeight(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common: return 40;
            case Rarity.Uncommon: return 20;
            case Rarity.Rare: return 10;
            case Rarity.Epic: return 5;
            case Rarity.Legendary: return 1;
            default: return 0;
        }
    }
}
