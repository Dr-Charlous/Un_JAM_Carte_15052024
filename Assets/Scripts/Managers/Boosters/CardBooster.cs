using DG.Tweening;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/CardBooster", order = 1)]
public class CardBooster : ScriptableObject
{
    public CardData[] CardsDropScript;
    public int NumberDropMin;
    public int NumberDropMax;

    
}
