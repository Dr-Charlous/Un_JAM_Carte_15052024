using System;
using System.Collections.Generic;
using LuniLibrary.SingletonClassBase;
using Managers.Fusions;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public CardManager CardManager { get; private set; }
        [field: SerializeField] public FusionManager FusionManager { get; private set; }
        [field: SerializeField] public TimeManager TimeManager { get; private set; }
        [field: SerializeField] public MenuManager MenuManager { get; private set; }
        
        public List<Card> Coins { get; } = new List<Card>();

        protected override void InternalAwake()
        {
        }
    }
}