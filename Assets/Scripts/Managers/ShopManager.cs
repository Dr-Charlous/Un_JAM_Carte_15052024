using System.Collections.Generic;
using Audio;
using Managers.Boosters;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] CardData _cardDataMoney;
        [SerializeField] CardBooster _boosterData;
        [SerializeField] GameObject _cardPrefab;
        [SerializeField] GameObject _boosterPrefab;

        public void Shop(List<CardAssign> cardAssign)
        {
            AudioManager.Instance.PlaySound("Vente");
        
            for (int i = 0; i < cardAssign.Count; i++)
            {
                if (cardAssign[i].CardData != _cardDataMoney)
                {
                    for (int j = 0; j < cardAssign[i].CardData.CostSell; j++)
                    {
                        GameObject obj = Instantiate(_cardPrefab);
                        obj.GetComponent<CardAssign>().CardData = _cardDataMoney;
                    }

                    Destroy(cardAssign[i].gameObject);
                }
                else
                {
                    GameObject obj = Instantiate(_boosterPrefab);
                    obj.GetComponent<BoosterAssign>().BoosterData = _boosterData;

                    Destroy(cardAssign[i].gameObject);
                }
            }
        }
    }
}
