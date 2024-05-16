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
                Destroy(gameObject);
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
