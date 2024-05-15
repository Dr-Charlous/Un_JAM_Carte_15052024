using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool IsBeingDropped { get; set; }
    public Card ChildCard { get; set; }

    [Header("References")] 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _childTransform;
    [SerializeField] private Transform _objectToScale;

    private Vector3 _initScale;
    private Card _parentCard;

    private void Start()
    {
        _initScale = transform.localScale;
    }

    private void Update()
    {
        HandleBeingDropped();
    }

    private void HandleBeingDropped()
    {
        if (IsBeingDropped)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(_boxCollider2D.bounds.center, _boxCollider2D.size, 0);

            bool hasNewParent = false;

            foreach (Collider2D hit in hits)
            {
                if (CheckForCards(hit, out Card card))
                {
                    continue;
                }

                if (_parentCard != null)
                {
                    DiscardParenting();
                }

                _parentCard = card;

                transform.parent = card._childTransform;
                transform.position = card._childTransform.position;

                _parentCard.ChildCard = this;

                _parentCard._boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, 1);
                _parentCard._boxCollider2D.size = new Vector2(_boxCollider2D.size.x, 0.5f);

                // CancelCardScale();
                hasNewParent = true;

                break;
            }

            if (hasNewParent == false)
            {
                if (_parentCard != null)
                {
                    DiscardParenting();
                }

                _boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, ChildCard == null ? 0 : 1);
                _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, ChildCard == null ? 2.5f : 0.5f);

                _parentCard = null;
                transform.parent = null;
            }

            IsBeingDropped = false;
        }
    }

    private bool CheckForCards(Collider2D hit, out Card card)
    {
        card = null;
            
        if (hit == _boxCollider2D)
        {
            return true;
        }

        if (!hit.TryGetComponent(out card))
        {
            return true;
        }

        if (card == ChildCard)
        {
            return true;
        }

        if (card.ChildCard != null)
        {
            return true;
        }

        return false;
    }

    private void DiscardParenting()
    {
        _parentCard._boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, 0);
        _parentCard._boxCollider2D.size = new Vector2(_boxCollider2D.size.x, 2.5f);

        _parentCard.ChildCard = null;
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