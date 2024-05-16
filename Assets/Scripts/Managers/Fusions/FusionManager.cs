using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Managers.Fusions
{
    public class FusionManager : MonoBehaviour
    {
        [field:SerializeField] public FusionData[] FusionDatas { get; private set; }

        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private float _spawnCardRadius;
        
        public void HandleFusion(List<CardAssign> cards)
        {
            foreach (FusionData currentFusion in FusionDatas)
            {
                int currentCardsNb = 0;
                bool isInCondition = false;
                List<CardData> conditions = new List<CardData>(currentFusion.Recipe.Count);

                for (var index = 0; index < cards.Count; index++)
                {
                    var card = cards[index];
                    foreach (CardData condition in currentFusion.Recipe)
                    {
                        if (conditions.Contains(condition))
                        {
                            isInCondition = false;
                            continue;
                        }

                        if (condition != card.CardData)
                        {
                            isInCondition = false;
                            continue;
                        }

                        conditions.Add(condition);

                        isInCondition = true;

                        currentCardsNb++;

                        break;
                    }

                    if (isInCondition == false)
                    {
                        break;
                    }

                    if (CheckForCardNb(currentCardsNb, currentFusion) && index >= cards.Count)
                    {
                        break;
                    }
                }

                if (CheckForCardNb(currentCardsNb, currentFusion) && isInCondition)
                {
                    StartCoroutine(MakeFusion(cards, currentFusion));
                }
            }
        }

        private bool CheckForCardNb(int currentCardsNb, FusionData currentFusion)
        {
            return currentCardsNb >= currentFusion.Recipe.Count;
        }

        private IEnumerator MakeFusion(List<CardAssign> cards, FusionData currentFusion)
        {
            Debug.Log("FUSION");
            
            foreach (CardAssign card in cards)
            {
                if(card.TryGetComponent(out Card cardComponent))
                {
                    if (GameManager.Instance.CardManager.GetHigherStackParent(cardComponent) != cardComponent)
                    {
                        cardComponent.CanMove = false;
                    }

                    cardComponent.CanDropCardOnThis = false;
                }
            }
            
            yield return new WaitForSeconds(currentFusion.Time);
            
            foreach (CardAssign card in cards)
            {
                StopAllCoroutines();
                Destroy(card.gameObject);
            }

            foreach (CardData card in currentFusion.Result)
            {
                Vector2 randomPos = Random.insideUnitCircle * _spawnCardRadius;
                GameObject newCard = Instantiate(_cardPrefab);

                newCard.transform.DOJump(randomPos, GameManager.Instance.CardManager.JumpTweenStrength, 1, GameManager.Instance.CardManager.JumpTweenDuration);
                newCard.GetComponent<CardAssign>().CardData = card;
            }
        }
    }
}
