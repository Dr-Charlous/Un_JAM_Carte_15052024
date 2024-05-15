using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoosterAssign : MonoBehaviour
{
    [SerializeField] CardBooster _boosterData;

    private void OnMouseDown()
    {
        if (!_boosterData.CardDropper())
            Destroy(gameObject);
    }
}
