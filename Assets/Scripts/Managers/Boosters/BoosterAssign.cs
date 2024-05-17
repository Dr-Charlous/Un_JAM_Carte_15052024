using DG.Tweening;
using UnityEngine;

namespace Managers.Boosters
{
    public class BoosterAssign : MonoBehaviour
    {
        public CardBooster BoosterData;

        int _dropNumber;

        private void Start()
        {
            _dropNumber = Random.Range(BoosterData.NumberDropMin, BoosterData.NumberDropMax+1);
        }

        private void OnMouseDown()
        {
            _dropNumber = CardDropper();
            
            if (_dropNumber <= 0)
            {
                transform.DOScale(0, 0.1f).OnComplete(() => Destroy(gameObject));
            }
            else
            {
                transform.DOKill();
                transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
            }
        }

        private int CardDropper()
        {
            int number = _dropNumber;
            
            if (number > 0)
            {
                int rnd = Random.Range(0, BoosterData.CardsDropScript.Length);
                GameManager.Instance.CardManager.SpawnCard(transform.position, BoosterData.CardsDropScript[rnd]);

                number--;
            }
            
            return number;
        }
    }
}
