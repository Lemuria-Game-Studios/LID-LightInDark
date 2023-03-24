using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public Transform target; 
        private EnemySensor _sensor;
        private EnemyMover _mover;

        private void Awake()
        {
            _sensor = GetComponent<EnemySensor>();
            _mover = GetComponent<EnemyMover>();
        }

        private void Update()
        {
            CheckIfPlayerDetected();
        }

        private void CheckIfPlayerDetected()
        {
            if (_sensor.isPlayerDetected)
            {
                target = _sensor.target;
                _mover.target = target;
                _mover.isMovingToPlayer = true;
            }
            else
            {
                _mover.isMovingToPlayer = false;
            }
        }
    }
}
