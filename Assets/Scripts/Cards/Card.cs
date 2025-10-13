using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "NewCard", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite icon;
    public Rarity rarity;

    public void AddToTower()
    {
        Debug.Log($"{cardName} used!");
    }
}
