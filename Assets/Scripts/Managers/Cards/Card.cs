using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using DG.Tweening;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public bool IsBeingDropped { get; set; }
    public bool CanMove { get; set; }
    public bool CanDropCardOnThis { get; set; }
    public Card ChildCard { get; set; }
    public Card ParentCard { get; private set; }
    public Vector2 InitPos { get; set; }

    [Header("References")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _childTransform;
    [SerializeField] private Image _timerFillImage;

    private CardAssign _cardAssign;
    private readonly Tuple<float, float> _normalCollider = new Tuple<float, float>(0, 2.1f);
    private readonly Tuple<float, float> _reducedCollider = new Tuple<float, float>(0.8f, 0.45f);

    private void Start()
    {
        CanMove = true;
        CanDropCardOnThis = true;
        _cardAssign = GetComponent<CardAssign>();

        ChangeCollider(_normalCollider.Item1, _normalCollider.Item2);
    }

    private void Update()
    {
        HandleBeingDroppedOnCard();
    }

    private void HandleBeingDroppedOnCard()
    {
        if (IsBeingDropped)
        {
            AudioManager.Instance.PlaySound("Poser");
            
            Collider2D[] hits = Physics2D.OverlapBoxAll(_boxCollider2D.bounds.center, _boxCollider2D.size, 0, GameManager.Instance.CardManager.InteractiveLayers);

            bool hasNewParent = false;

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out ShopManager shop))
                {
                    if((shop.Type == ShopType.Booster && _cardAssign.CardData.CardType == CardType.Card) 
                       || (shop.Type == ShopType.Coins && _cardAssign.CardData.CardType == CardType.Coin)
                       || _cardAssign.CardData.CanBeSold == false)
                    {
                        CancelMove();
                        
                        return;
                    }
                    
                    DiscardParenting();
                    
                    List<CardAssign> children = GameManager.Instance.CardManager.GetAllChildren(this);
                    shop.Shop(children);
                    
                    IsBeingDropped = false;

                    return;
                }

                if (hit.CompareTag("Wall"))
                {
                    CancelMove();

                    return;
                }

                if (CheckForCards(hit, out Card card))
                {
                    continue;
                }

                if (card.CanDropCardOnThis == false)
                {
                    continue;
                }

                DiscardParenting();

                HandleNewParent(card);

                // CancelCardScale();
                hasNewParent = true;

                break;
            }

            if (hasNewParent == false)
            {
                DiscardParenting();

                ChangeCollider(ChildCard == null ? _normalCollider.Item1 : _reducedCollider.Item1, ChildCard == null ? _normalCollider.Item2 : _reducedCollider.Item2);
            }

            HandleCombo(this);

            IsBeingDropped = false;
        }
    }

    public void CancelMove()
    {
        transform.DOKill();
        transform.DOMove(InitPos, 0.2f);
    }

    private void HandleCombo(Card card)
    {
        List<CardAssign> cards = GetChildren(card);

        if (cards.Count > 1)
        {
            GameManager.Instance.FusionManager.HandleFusion(cards);
        }
    }

    private List<CardAssign> GetChildren(Card card)
    {
        Card higherParent = GameManager.Instance.CardManager.GetHigherStackParent(card);
        return GameManager.Instance.CardManager.GetAllChildren(higherParent);
    }

    private void HandleNewParent(Card card)
    {
        ParentCard = card;

        transform.parent = card._childTransform;
        transform.position = card._childTransform.position;

        ParentCard.ChildCard = this;

        ParentCard.ChangeCollider(_reducedCollider.Item1, _reducedCollider.Item2);
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
        if (ParentCard != null)
        {
            ParentCard.ChangeCollider(_normalCollider.Item1, _normalCollider.Item2);
            ParentCard.ChildCard = null;
            HandleCombo(ParentCard);
            
            ParentCard = null;
            transform.parent = null;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void DisplayTimer(float time)
    {
        _timerFillImage.transform.parent.gameObject.SetActive(true);
        _timerFillImage.fillAmount = 1;

        _timerFillImage.DOFillAmount(0, time);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider2D.bounds.center, _boxCollider2D.size);
    }
}