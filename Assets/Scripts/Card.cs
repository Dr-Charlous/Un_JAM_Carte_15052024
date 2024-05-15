using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool IsBeingDropped { get; set; }

    [Header("References")] 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _childTransform;
    [SerializeField] private Transform _objectToScale;

    private Vector3 _initScale;
    private Card _childCard;
    private Card _parentCard;

    private void Start()
    {
        _initScale = transform.localScale;
    }

    private void Update()
    {
        if (IsBeingDropped)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _boxCollider2D.size, 0);

            bool landedOnCard = false;

            foreach (Collider2D hit in hits)
            {
                if (hit != _boxCollider2D)
                {
                    Card cardParent = hit.GetComponent<Card>();

                    if (cardParent._childCard == null && hit != _childCard)
                    {
                        transform.parent = hit.transform;

                        if (_parentCard != null)
                        {
                            _parentCard._childCard = null;
                        }
                        
                        cardParent._childCard = this;
                        _parentCard = cardParent;

                        transform.position = cardParent._childTransform.position;

                        landedOnCard = true;

                        // cardParent.CancelCardScale();
                    }
                }
            }

            if (landedOnCard == false)
            {
                transform.parent = null;

                if (_parentCard != null)
                {
                    _parentCard._childCard = null;
                }
                _parentCard = null;
            }
            
            // CancelCardScale();
            IsBeingDropped = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _boxCollider2D.size);
    }

    public void DoHoverCardScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;

        _objectToScale.DOKill();
        _objectToScale.DOScale(cardManager.HoverTweenStrength, cardManager.HoverTweenDuration);
    }

    public void DoMoveCardScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;

        _objectToScale.DOKill();
        _objectToScale.DOScale(cardManager.MoveTweenStrength, cardManager.MoveTweenDuration);
    }

    public Tween CancelCardScale()
    {
        CardManager cardManager = GameManager.Instance.CardManager;

        _objectToScale.DOKill();
        Tween scaleDownTween = _objectToScale.DOScale(_initScale, cardManager.HoverTweenDuration);

        return scaleDownTween;
    }
}