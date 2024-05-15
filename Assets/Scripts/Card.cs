using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Tween _punchTween;

    public Tween PunchScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;
        
        _punchTween.Kill();
        _punchTween = transform.DOPunchScale(cardManager.EndMovePunchStrength * Vector3.one, cardManager.EndMovePunchDuration);

        return _punchTween;
    }
}
