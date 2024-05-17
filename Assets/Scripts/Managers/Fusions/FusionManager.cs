using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using DG.Tweening;
using UnityEngine;

namespace Managers.Fusions
{
    public class FusionManager : MonoBehaviour
    {
        [field:SerializeField] public FusionData[] FusionDatas { get; private set; }
        
        private Vector3 _lastCardPosition;
        
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
                    break;
                }
            }
        }

        private bool CheckForCardNb(int currentCardsNb, FusionData currentFusion)
        {
            return currentCardsNb >= currentFusion.Recipe.Count;
        }

        private IEnumerator MakeFusion(List<CardAssign> cards, FusionData currentFusion)
        {
            AudioManager.Instance.PlaySound("Fusion");
            
            foreach (CardAssign card in cards)
            {
                if(card.TryGetComponent(out Card cardComponent))
                {
                    Card higherStackParent = GameManager.Instance.CardManager.GetHigherStackParent(cardComponent);
                    
                    higherStackParent.DisplayTimer(currentFusion.Time);
                    
                    if (higherStackParent != cardComponent)
                    {
                        cardComponent.CanMove = false;
                        
                        _lastCardPosition = card.transform.position;
                    }

                    cardComponent.CanDropCardOnThis = false;
                }
            }
            
            yield return new WaitForSeconds(currentFusion.Time);
            
            foreach (CardAssign card in cards)
            {
                StopCoroutine(MakeFusion(cards, currentFusion));
                card.transform.DOScale(0, 0.1f).OnComplete(() => Destroy(card.gameObject));
            }

            foreach (CardData card in currentFusion.Result)
            {
                GameManager.Instance.CardManager.SpawnCard(_lastCardPosition, card);
            }
        }
    }
}
