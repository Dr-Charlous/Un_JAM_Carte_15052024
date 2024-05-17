using System;
using System.Collections.Generic;
using LuniLibrary.SingletonClassBase;
using Managers.Fusions;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public CardManager CardManager { get; private set; }
        [field: SerializeField] public FusionManager FusionManager { get; private set; }
        [field: SerializeField] public TimeManager TimeManager { get; private set; }
        [field: SerializeField] public MenuManager MenuManager { get; private set; }

        [SerializeField] private TMP_Text _coinsText;
        
        public List<Card> Coins { get; } = new List<Card>();

        protected override void InternalAwake()
        {
        }

        private void Start()
        {
            UpdateCoinsUI();
        }

        public void AddCoins(Card newCard)
        {
            Coins.Add(newCard);

            UpdateCoinsUI();
        }

        public void UpdateCoinsUI()
        {
            _coinsText.text = $"{Coins.Count.ToString()} / {TimeManager.CoinsNeededEndTurn}";
        }
    }
}