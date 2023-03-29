using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public Transform target;

        private enum EnemyStates
        { Wandering, MovingToPlayer, Aiming, Attacking }
        private EnemyStates _state = EnemyStates.Wandering;    
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
            SetState();
            SetTarget();
            SetIfCanMove();
            SetAttacking();
        }

        private void SetState()
        {
            if (_attacker.isAttacking) _state = EnemyStates.Attacking;
            else if (_attacker.isInRange) _state = EnemyStates.Aiming;
            else if (_sensor.isPlayerDetected) _state = EnemyStates.MovingToPlayer;
            else _state = EnemyStates.Wandering;
        }

        private void SetIfCanMove()
        {
            switch (_state)
            {
                case EnemyStates.Wandering:
                    _mover.isMovingToPlayer = false;
                    break;
                case EnemyStates.MovingToPlayer:
                    _mover.isMovingToPlayer = true;
                    break;
                case EnemyStates.Aiming:
                    _nav.velocity = Vector3.zero;
                    _mover.canMove = true;
                    break;
                case EnemyStates.Attacking:
                    _nav.velocity = Vector3.zero;
                    _mover.canMove = false;
                    break;
            }
        }

        private void SetTarget()
        {
            if (_state == EnemyStates.Wandering) return;
            
            target = _sensor.target;
            _mover.target = target;
            _attacker.target = target;
        }
        
        private void SetAttacking()
        {
            if (_state == EnemyStates.Aiming)
            {
                _attacker.Attack();
            }
        }
    }
}
