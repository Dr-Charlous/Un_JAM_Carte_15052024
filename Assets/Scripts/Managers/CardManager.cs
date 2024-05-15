using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class CardManager : MonoBehaviour
    {
        [Header("Hover Tween Values")] 
        [SerializeField] private float _hoverTweenDuration;
        [SerializeField] private float _hoverTweenStrength;
        
        [Header("Move Tween Values")] 
        [SerializeField] private float _moveTweenDuration;
        [SerializeField] private float _moveTweenStrength;

        #region Properties

        public float MoveTweenDuration => _moveTweenDuration;
        public float MoveTweenStrength => _moveTweenStrength;

        public float HoverTweenDuration => _hoverTweenDuration;

        public float HoverTweenStrength => _hoverTweenStrength;

        #endregion
    }
}