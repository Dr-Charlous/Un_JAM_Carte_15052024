using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class CardManager : MonoBehaviour
    {
        [Header("End Move Tween Values")] 
        [SerializeField] private float _endMovePunchDuration;
        [SerializeField] private float _endMovePunchStrength;

        public float EndMovePunchDuration => _endMovePunchDuration;
        public float EndMovePunchStrength => _endMovePunchStrength;
    }
}