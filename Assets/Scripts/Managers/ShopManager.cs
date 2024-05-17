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
        [Header("Shop display")] [SerializeField]
        private TMP_Text _nameText;

        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private string _shopName;

        [field: Header("Shop Data")]
        [field: SerializeField]
        public ShopType Type { get; private set; }

        [SerializeField] private CardData _cardDataMoney;
        [SerializeField] private CardBooster _boosterData;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private GameObject _boosterPrefab;
        [SerializeField] private int _boosterPrice;

        private int _currentPrice;

        private void Start()
        {
            _currentPrice = _boosterPrice;

            UpdatePriceText();
            if (_nameText != null) _nameText.text = _shopName;
        }

        private void UpdatePriceText()
        {
            if (_priceText != null) _priceText.text = _currentPrice.ToString();
        }

        public async void Shop(List<CardAssign> cardAssign)
        {
            AudioManager.Instance.PlaySound("Vente");

            for (int i = 0; i < cardAssign.Count; i++)
            {
                Card cardComponent = cardAssign[i].GetComponent<Card>();

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
                            newPos = new Vector3(lastCoinPos.x, lastCoinPos.y + 0.5f, 0);
                        }
                        else
                        {
                            newPos = new Vector3(transform.position.x + 2, transform.position.y - 4.5f, 0);
                        }

                        newCard.transform.DOJump(newPos, 1.15f, 1, 0.15f).OnComplete(() => newCard.IsBeingDropped = true);
                        newCard.GetComponent<CardAssign>().CardData = _cardDataMoney;

                        GameManager.Instance.AddCoins(newCard);

                        await Task.Delay(200);
                    }
                }
                else
                {
                    _currentPrice--;
                    UpdatePriceText();

                    GameManager.Instance.Coins.Remove(cardComponent);
                    GameManager.Instance.UpdateCoinsUI();

                    if (_currentPrice <= 0)
                    {
                        GameObject newBooster = Instantiate(_boosterPrefab, transform.position, Quaternion.identity);

                        Vector3 newPos = new Vector3(transform.position.x + 2, transform.position.y - 4.5f, 0);
                        newBooster.transform.DOJump(newPos, 1.15f, 1, 0.15f);

                        newBooster.GetComponent<BoosterAssign>().BoosterData = _boosterData;

                        _currentPrice = _boosterPrice;
                        UpdatePriceText();
                    }
                }
                
                await Task.Delay(200);
            }

            foreach (CardAssign card in cardAssign)
            {
                Destroy(card.gameObject);
            }
        }
    }

    public enum ShopType
    {
        Coins,
        Booster
    }
}