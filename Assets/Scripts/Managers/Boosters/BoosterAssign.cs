using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoosterAssign : MonoBehaviour
{
    public CardBooster BoosterData;

    [SerializeField] GameObject _cardPrefab;

    int _dropNumber;

    private void Start()
    {
        _dropNumber = Random.Range(BoosterData.NumberDropMin, BoosterData.NumberDropMax+1);
    }

    private void OnMouseDown()
    {
        _dropNumber = BoosterData.CardDropper(_cardPrefab, _dropNumber);
        if (_dropNumber <= 0)
            Destroy(gameObject);
    }
}
