using System;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] int _coinsNeededEndTurn;
        [SerializeField] int _coinIncrementPerTurn;
        [SerializeField] float _time;
        [SerializeField] TMP_Text _timerText;

        float _timeSet;
        bool _isCheck;

        public int CoinsNeededEndTurn => _coinsNeededEndTurn;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (_timeSet >= 0)
            {
                _timeSet -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(_timeSet / 60);
                int seconds = Mathf.FloorToInt(_timeSet % 60);

                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            }
            else if (!_isCheck)
            {
                CheckEndTimer(CoinsNeededEndTurn);
                _coinsNeededEndTurn = CoinsNeededEndTurn + _coinIncrementPerTurn;
                _isCheck = true;
            }
        }

        public void Initialize()
        {
            _isCheck = false;
            _timeSet = _time;
            _timerText.text = _timeSet.ToString();
        }

        void CheckEndTimer(int numberCoinsNeeded)
        {
            if (GameManager.Instance.Coins.Count >= numberCoinsNeeded)
            {
                GameManager.Instance.MenuManager.ShowContinueUI();
            }
            else
            {
                GameManager.Instance.MenuManager.ShowLoseUI();
            }
        }
    }
}