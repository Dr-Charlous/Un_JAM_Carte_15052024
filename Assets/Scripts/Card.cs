using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Vector2 _initScale;
    private Tween _hoverScaleTween;
    private Tween _moveScaleTween;
    private Tween _scaleDownTween;

    private void Start()
    {
        _initScale = transform.localScale;
    }

    public void DoHoverCardScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;
        
        _hoverScaleTween.Kill();
        _hoverScaleTween = transform.DOScale(cardManager.HoverTweenStrength, cardManager.HoverTweenDuration);
    }
    
    public void DoMoveCardScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;
        
        _moveScaleTween.Kill();
        _moveScaleTween = transform.DOScale(cardManager.MoveTweenStrength, cardManager.MoveTweenDuration);
    }
    
    public Tween CancelCardScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;
        
        _scaleDownTween.Kill();
        _scaleDownTween = transform.DOScale(_initScale, cardManager.HoverTweenDuration);

        return _scaleDownTween;
    }
}
