using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoosterAssign : MonoBehaviour
{
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] CardBooster _boosterData;
    int _dropNumber;

    private void Start()
    {
        _dropNumber = _boosterData.NumberDrop;
    }

    private void OnMouseDown()
    {
        if (!_boosterData.CardDropper(_cardPrefab, _dropNumber))
            Destroy(gameObject);
    }
}
