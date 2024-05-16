using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] CardAssign _cardAssignMoney;
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] GameObject _boosterPrefab;

    public void Shop(List<CardAssign> cardAssign)
    {
        for (int i = 0; i < cardAssign.Count; i++)
        {
            if (cardAssign[i] == _cardAssignMoney)
            {
                for (int j = 0; j < cardAssign[i].CardData.CostSell; j++)
                {
                    GameObject obj = Instantiate(_cardPrefab);
                    obj.GetComponent<CardAssign>().CardData = _cardAssignMoney.CardData;
                }

                Destroy(cardAssign[i].gameObject);
            }
            else
            {
                Instantiate(_boosterPrefab);

                Destroy(cardAssign[i].gameObject);
            }
        }
    }
}
