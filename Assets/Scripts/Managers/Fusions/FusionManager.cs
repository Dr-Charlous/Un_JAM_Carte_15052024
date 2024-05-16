using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers.Fusions
{
    public class FusionManager : MonoBehaviour
    {
        [field:SerializeField] public FusionData[] FusionDatas { get; private set; }

        [SerializeField] GameObject _cardPrefab;
        
        public void HandleFusion(List<CardAssign> cards)
        {
            foreach (FusionData currentFusion in FusionDatas)
            {
                int currentCardsNb = 0;
                List<CardData> conditions = new List<CardData>(currentFusion.Recipe.Count);

                foreach (CardAssign card in cards)
                {
                    foreach (CardData condition in currentFusion.Recipe)
                    {
                        if (conditions.Contains(condition))
                        {
                            continue;
                            
                        }
                        if (condition != card.CardData)
                        {
                            continue;
                        }
                        
                        conditions.Add(condition);

                        currentCardsNb++;
                        
                        break;
                    }
                    

                    if (CheckForCardNb(currentCardsNb, currentFusion))
                    {
                        break;
                    }
                }

                if (CheckForCardNb(currentCardsNb, currentFusion))
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
            yield return new WaitForSeconds(currentFusion.Time);
            
            foreach (CardAssign card in cards)
            {
                StopAllCoroutines();
                Destroy(card.gameObject);
            }

            foreach (CardData card in currentFusion.Result)
            {
                GameObject newCard = Instantiate(_cardPrefab);
                newCard.GetComponent<CardAssign>().CardData = card;
            }
        }
    }
}
