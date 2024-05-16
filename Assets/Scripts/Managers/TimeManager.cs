using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] float _time;

        float _timeSet;

        private void Start()
        {
            _timeSet = _time;
        }

        private void Update()
        {
            if (_timeSet > 0)
                _timeSet -= Time.deltaTime;
        }
    }
}
