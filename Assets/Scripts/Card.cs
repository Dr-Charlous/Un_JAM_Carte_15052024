using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool IsBeingDropped { get; set; }

    public Card ChildCard
    {
        get => _childCard;
        set
        {
            _childCard = value;
            
            _boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, value != null ? 1 : 0);
            _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, value != null ? 0.5f : 2.5f);
        }
    }

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
            Collider2D[] hits = Physics2D.OverlapBoxAll(_boxCollider2D.bounds.center, _boxCollider2D.size, 0);

            bool hasNewParent = false;

            foreach (Collider2D hit in hits)
            {
                if (hit == _boxCollider2D)
                {
                    continue;
                }

                if (!hit.TryGetComponent(out Card card))
                {
                    continue;
                }

                if (card == _childCard)
                {
                    continue;
                }

                if (card.ChildCard != null)
                {
                    continue;
                }

                _parentCard = card;

                transform.parent = card._childTransform;
                transform.position = card._childTransform.position;

                _parentCard.ChildCard = this;

                // CancelCardScale();
                hasNewParent = true;

                break;
            }

            if (hasNewParent == false)
            {
                if (_parentCard != null)
                {
                    _parentCard.ChildCard = null;
                }

                _parentCard = null;
                transform.parent = null;
            }

            IsBeingDropped = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider2D.bounds.center, _boxCollider2D.size);
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