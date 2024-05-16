using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Fusion", order = 1)]
public class FusionData : ScriptableObject
{
    public List<CardData> Recipe;
    public List<CardData> Result;
    public int Time;
}