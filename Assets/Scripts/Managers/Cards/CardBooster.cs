using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/CardBooster", order = 1)]
public class CardBooster : ScriptableObject
{
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] int _numberDrop;
    public CardID[] CardsDropScript;

    public bool CardDropper()
    {
        if (_numberDrop > 0)
        {
            GameObject obj = Instantiate(_cardPrefab);

            int rnd = UnityEngine.Random.Range(0, CardsDropScript.Length);

            obj.GetComponent<CardAssign>().CardID = CardsDropScript[rnd];

            _numberDrop--;
            return true;
        }
        else
            return false;
    }
}
