using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public Transform Target;

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
            if (_attacker.IsAttacking) _state = EnemyStates.Attacking;
            else if (_attacker.IsInRange) _state = EnemyStates.Aiming;
            else if (_sensor.IsPlayerDetected) _state = EnemyStates.MovingToPlayer;
            else _state = EnemyStates.Wandering;
        }

        private void SetIfCanMove()
        {
            switch (_state)
            {
                case EnemyStates.Wandering:
                    _mover.isMovingToPlayer = false;
                    _mover.canMove = true;
                    break;
                case EnemyStates.MovingToPlayer:
                    _mover.isMovingToPlayer = true;
                    _mover.canMove = true;
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
            
            Target = _sensor.Target;
            _mover.target = Target;
            _attacker.Target = Target;
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
