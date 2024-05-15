using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class CardManager : MonoBehaviour
    {
        [Header("End Move Tween Values")] 
        [SerializeField] private float _endMovePunchDuration;
        [SerializeField] private float _endMovePunchStrength;

        public void EndMovePunch(Card card)
        {
            card.transform.DOPunchScale(Vector3.one * _endMovePunchStrength, _endMovePunchDuration);
        }
    }
}