using System;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] float _time;
        [SerializeField] TMP_Text _timerText;

        float _timeSet;

        private void Start()
        {
            _timeSet = _time;
            _timerText.text = _timeSet.ToString();
        }

        private void Update()
        {
            if (_timeSet > 0)
            {
                _timeSet -= Time.deltaTime;
                
                int minutes = Mathf.FloorToInt(_timeSet / 60);
                int seconds = Mathf.FloorToInt(_timeSet % 60);
    
                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}