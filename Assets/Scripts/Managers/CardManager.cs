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
        
        [Header("Hover Tween Values")] 
        [SerializeField] private float _hoverTweenDuration;
        [SerializeField] private float _hoverTweenStrength;
        
        [Header("Move Tween Values")] 
        [SerializeField] private float _moveTweenDuration;
        [SerializeField] private float _moveTweenStrength;

        #region Properties

        public float MoveTweenDuration => _moveTweenDuration;
        public float MoveTweenStrength => _moveTweenStrength;

        public float HoverTweenDuration => _hoverTweenDuration;

        public float HoverTweenStrength => _hoverTweenStrength;
        public LayerMask CardLayer => _cardLayer;

        #endregion

        public Card GetHigherStackParent(Card card)
        {
            if (card.ParentCard == null)
            {
                return card;
            }

            return GetHigherStackParent(card.ParentCard);
        }

        public Card[] GetAllChildren(Card parentCard)
        {
            List<Card> allChildren = new List<Card>();
            Card currentCard = parentCard.ChildCard;

            allChildren.Add(parentCard);
            
            while (currentCard != null)
            {
                allChildren.Add(currentCard);
                currentCard = currentCard.ChildCard;
            }
            
            Card[] children = allChildren.ToArray();
            
            for (int index = 0; index < children.Length; index++)
            {
                Card child = children[index];
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y,  child.transform.position.z - 0.1f * index);
            }

            return children;
        }
    }
}