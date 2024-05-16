using System;
using LuniLibrary.SingletonClassBase;
using Managers.Fusions;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public CardManager CardManager { get; private set; }
        [field: SerializeField] public FusionManager FusionManager { get; private set; }

        protected override void InternalAwake()
        {
        }
    }
}