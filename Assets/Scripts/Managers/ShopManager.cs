using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using DG.Tweening;
using Managers.Boosters;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Shop display")]
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private string _shopName;
        
        [Header("Shop Data")]
        [SerializeField] private CardData _cardDataMoney;
        [SerializeField] private CardBooster _boosterData;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private GameObject _boosterPrefab;

        private int _currentPrice;

        private void Start()
        {
            // _currentPrice = 
            _nameText.text = _shopName;
        }

        public async void Shop(List<CardAssign> cardAssign)
        {
            AudioManager.Instance.PlaySound("Vente");
        
            for (int i = 0; i < cardAssign.Count; i++)
            {
                if (cardAssign[i].CardData != _cardDataMoney)
                {
                    for (int j = 0; j < cardAssign[i].CardData.CostSell; j++)
                    {
                        Card newCard = Instantiate(_cardPrefab, transform.position, Quaternion.identity);
                        
                        newCard.transform.DOScale(1, 0.1f);

                        Vector3 newPos = Vector3.zero;
                        
                        if (GameManager.Instance.Coins.Count > 0)
                        {
                            Vector3 lastCoinPos = GameManager.Instance.Coins[^1].transform.position;
                            newPos =  new Vector3(lastCoinPos.x, lastCoinPos.y + 0.5f, 0);
                        }
                        else
                        {
                            newPos = new Vector3(transform.position.x, transform.position.y - 3, 0);
                        }
                        
                        newCard.transform.DOJump(newPos, 1.15f, 1,0.15f).OnComplete(() => newCard.IsBeingDropped = true);
                        newCard.GetComponent<CardAssign>().CardData = _cardDataMoney;
                        
                        GameManager.Instance.Coins.Add(newCard);

                        await Task.Delay(1000);
                    }

                    Destroy(cardAssign[i].gameObject);
                }
                else
                {
                    GameManager.Instance.Coins.Remove(cardAssign[i].GetComponent<Card>());
                    
                    GameObject newBooster = Instantiate(_boosterPrefab);
                    newBooster.GetComponent<BoosterAssign>().BoosterData = _boosterData;

                    Destroy(cardAssign[i].gameObject);
                }
            }
        }
    }
}
