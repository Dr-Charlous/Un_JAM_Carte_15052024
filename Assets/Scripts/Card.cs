using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool IsBeingDropped { get; set; }
    public Card ChildCard { get; set; }
    public Card ParentCard { get; private set; }

    [Header("References")] 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _childTransform;
    [SerializeField] private Transform _objectToScale;

    private Vector3 _initScale;

    private void Start()
    {
        _initScale = transform.localScale;
    }

    private void Update()
    {
        HandleBeingDroppedOnCard();
        HandleBeingDroppedOnShop();
    }

    private void HandleBeingDroppedOnShop()
    {
        if (IsBeingDropped)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(_boxCollider2D.bounds.center, _boxCollider2D.size, 0);

            foreach (Collider2D hit in hits)
            {
                
            }

            IsBeingDropped = false;
        }
    }

    private void HandleBeingDroppedOnCard()
    {
        if (IsBeingDropped)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(_boxCollider2D.bounds.center, _boxCollider2D.size, 0, GameManager.Instance.CardManager.CardLayer);

            bool hasNewParent = false;

            foreach (Collider2D hit in hits)
            {
                if (CheckForCards(hit, out Card card))
                {
                    continue;
                }

                if (ParentCard != null)
                {
                    DiscardParenting();
                }

                HandleNewParent(card);

                // CancelCardScale();
                hasNewParent = true;

                break;
            }

            if (hasNewParent == false)
            {
                if (ParentCard != null)
                {
                    DiscardParenting();
                }

                ChangeCollider(ChildCard == null ? 0 : 1, ChildCard == null ? 2.5f : 0.5f);

                ParentCard = null;
                transform.parent = null;
            }

            HandleCombo();

            IsBeingDropped = false;
        }
    }

    private void HandleCombo()
    {
        Card higherParent = GameManager.Instance.CardManager.GetHigherStackParent(this);
        List<CardAssign> allChild = GameManager.Instance.CardManager.GetAllChildren(higherParent);

        if (allChild.Count > 1)
        {
            GameManager.Instance.FusionManager.HandleFusion(allChild);
        }
    }

    private void HandleNewParent(Card card)
    {
        ParentCard = card;

        transform.parent = card._childTransform;
        transform.position = card._childTransform.position;

        ParentCard.ChildCard = this;

        ParentCard.ChangeCollider(1, 0.5f);
    }

    private void ChangeCollider(float offsetY, float sizeY)
    {
        _boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, offsetY);
        _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, sizeY);
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
        ParentCard.ChangeCollider(0,2.5f);

        ParentCard.ChildCard = null;
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