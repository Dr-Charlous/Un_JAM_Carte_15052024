using System.Collections.Generic;
using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class CardManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private LayerMask _interactiveLayers;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private float _minSpawnRadius;
        [SerializeField] private float _maxSpawnRadius;
        [SerializeField] private float _ySpawnOffset;

        [Header("Jump Spawn Tween Values")] 
        [SerializeField] private float _jumpTweenDuration;
        [SerializeField] private float _jumpTweenStrength;

        #region Properties

        public LayerMask InteractiveLayers => _interactiveLayers;

        #endregion

        public Card SpawnCard(Vector3 position, CardData card, SpawnDirection direction = SpawnDirection.Circle)
        {
            Vector2 randomPos = Random.insideUnitCircle * _maxSpawnRadius;
            Vector2 point = randomPos.normalized * Random.Range(_minSpawnRadius, _maxSpawnRadius);
            
            Card newCard = Instantiate(_cardPrefab, position, Quaternion.identity);

            newCard.transform.DOScale(1, 0.1f);

            Vector3 circleEndPos = newCard.transform.position + new Vector3(point.x, point.y, 0);
            Vector2 newPos = direction == SpawnDirection.Circle ? circleEndPos : new Vector3(position.x, position.y + _ySpawnOffset, 0);
                
            newCard.transform.DOJump(newPos, _jumpTweenStrength, 1,_jumpTweenDuration);
            newCard.GetComponent<CardAssign>().CardData = card;

            AudioManager.Instance.PlaySound("Spawn");

            return newCard;
        }
        
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