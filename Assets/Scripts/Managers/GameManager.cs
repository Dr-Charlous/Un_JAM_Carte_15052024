using LuniLibrary.SingletonClassBase;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public CardManager CardManager { get; private set; }

        protected override void InternalAwake()
        {
        }
    }
}