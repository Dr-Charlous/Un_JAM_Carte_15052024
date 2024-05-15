using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Fusion", order = 1)]
public class FusionData : ScriptableObject
{
    public CardData[] Reciepe;
    public CardData[] Result;
    public float Time;
}