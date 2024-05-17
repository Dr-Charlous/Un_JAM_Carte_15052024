using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Card", order = 1)]
public class CardData : ScriptableObject
{
    public string NameCard;
    public Sprite SpriteCard;
    public Color ColorCard;
    public int UseNumber;
    public int CostSell;
    public CardType CardType = CardType.Card;
}

public enum CardType
{
    Card,
    Coin
}
