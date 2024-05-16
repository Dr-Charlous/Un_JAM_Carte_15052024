using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/CardBooster", order = 1)]
public class CardBooster : ScriptableObject
{
    public CardData[] CardsDropScript;
    public int NumberDropMin;
    public int NumberDropMax;

    public bool CardDropper(GameObject prefab, int number)
    {
        if (number > 0)
        {
            GameObject obj = Instantiate(prefab);

            int rnd = UnityEngine.Random.Range(0, CardsDropScript.Length);

            obj.GetComponent<CardAssign>().CardData = CardsDropScript[rnd];

            number--;
            if (number > 0)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
