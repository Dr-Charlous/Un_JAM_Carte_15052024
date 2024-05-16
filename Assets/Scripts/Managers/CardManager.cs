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
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private float _minSpawnRadius;
        [SerializeField] private float _maxSpawnRadius;

        [Header("Jump Spawn Tween Values")] 
        [SerializeField] private float _jumpTweenDuration;
        [SerializeField] private float _jumpTweenStrength;

        #region Properties

        public LayerMask InteractiveLayers => _interactiveLayers;

        #endregion

        public void SpawnCard(Vector3 position, CardData card)
        {
            Vector2 randomPos = Random.insideUnitCircle * _maxSpawnRadius;
            GameObject newCard = Instantiate(_cardPrefab, position, Quaternion.identity);

            Vector2 point = randomPos.normalized * Random.Range(_minSpawnRadius, _maxSpawnRadius);

            newCard.transform.DOJump(newCard.transform.position + new Vector3(point.x, point.y, 0), _jumpTweenStrength, 1,_jumpTweenDuration);
            newCard.GetComponent<CardAssign>().CardData = card;
                
            AudioManager.Instance.PlaySound("Spawn");
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