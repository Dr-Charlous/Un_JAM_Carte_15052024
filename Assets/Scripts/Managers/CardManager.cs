using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class CardManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private LayerMask _cardLayer;
        [SerializeField] private LayerMask _shopLayer;
        
        [Header("Hover Tween Values")] 
        [SerializeField] private float _hoverTweenDuration;
        [SerializeField] private float _hoverTweenStrength;
        
        [Header("Move Tween Values")] 
        [SerializeField] private float _moveTweenDuration;
        [SerializeField] private float _moveTweenStrength;
        
        [Header("Jump Spawn Tween Values")] 
        [SerializeField] private float _jumpTweenDuration;
        [SerializeField] private float _jumpTweenStrength;

        #region Properties

        public float MoveTweenDuration => _moveTweenDuration;
        public float MoveTweenStrength => _moveTweenStrength;

        public float HoverTweenDuration => _hoverTweenDuration;

        public float HoverTweenStrength => _hoverTweenStrength;
        public LayerMask CardLayer => _cardLayer;
        public LayerMask ShopLayer => _shopLayer;

        public float JumpTweenDuration => _jumpTweenDuration;

        public float JumpTweenStrength => _jumpTweenStrength;

        #endregion

        public Card GetHigherStackParent(Card card)
        {
            if (card.ParentCard == null)
            {
                return card;
            }

            return GetHigherStackParent(card.ParentCard);
        }

        public List<CardAssign> GetAllChildren(Card parentCard)
        {
            List<Card> allChildren = new List<Card>();
            Card currentCard = parentCard.ChildCard;

            allChildren.Add(parentCard);
            
            while (currentCard != null)
            {
                allChildren.Add(currentCard);
                currentCard = currentCard.ChildCard;
            }

            List<CardAssign> cardsAssigns = new List<CardAssign>();

            for (var index = 0; index < allChildren.Count; index++)
            {
                Card child = allChildren[index];
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, -0.1f * index);
                
                cardsAssigns.Add(child.GetComponent<CardAssign>());
            }

            return cardsAssigns;
        }
    }
}