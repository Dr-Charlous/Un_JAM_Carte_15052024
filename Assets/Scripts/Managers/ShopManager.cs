using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] CardAssign _cardAssignMoney;
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] GameObject _boosterPrefab;

    public void Shop(CardAssign cardAssign)
    {
        if (cardAssign == _cardAssignMoney)
        {
            GameObject obj = Instantiate(_cardPrefab);
            obj.GetComponent<CardAssign>().CardData = _cardAssignMoney.CardData;

            Destroy(cardAssign.gameObject);
        }
        else
        {
            Instantiate(_boosterPrefab);

            Destroy(cardAssign.gameObject);
        }
    }
}
