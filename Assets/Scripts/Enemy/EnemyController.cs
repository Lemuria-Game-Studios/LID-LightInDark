using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public Transform target; 
        private EnemySensor _sensor;
        private EnemyMover _mover;
        private EnemyAttacker _attacker;
        private NavMeshAgent _nav;

        private void Awake()
        {
            _sensor = GetComponent<EnemySensor>();
            _mover = GetComponent<EnemyMover>();
            _attacker = GetComponent<EnemyAttacker>();
            _nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            CheckIfPlayerDetected();
        }

        private void CheckIfPlayerDetected()
        {
            _mover.canMove = !_attacker.isAttacking;

            if (_sensor.isPlayerDetected)
            {
                target = _sensor.target;
                _mover.target = target;
                _attacker.target = target;
                
                _mover.isMovingToPlayer = true;
                if (_attacker.isInRange)
                {
                    _nav.velocity = Vector3.zero;
                    _attacker.Attack();
                }
            }
            else
            {
                _mover.isMovingToPlayer = false;
            }
        }
    }
}
