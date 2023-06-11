using Common;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public Transform Target;

        private enum EnemyStates
        { Wandering, MovingToPlayer, Aiming, Attacking,Dead }
        [SerializeField] private EnemyStates _state = EnemyStates.Wandering;    
        private EnemySensor _sensor;
        private EnemyMover _mover;
        private EnemyAttacker _attacker;
        private NavMeshAgent _nav;
        private Health _health;

        private void Awake()
        {
            _sensor = GetComponent<EnemySensor>();
            _mover = GetComponent<EnemyMover>();
            _attacker = GetComponent<EnemyAttacker>();
            _nav = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            SetState();
            SetTarget();
            SetBooleans();
            SetAttacking();
        }

        private void SetState()
        {
            if (_health.isDead)_state = EnemyStates.Dead;
            else if (_attacker.IsAttacking) _state = EnemyStates.Attacking;
            else if (_attacker.IsInRange) _state = EnemyStates.Aiming;
            else if (_sensor.IsPlayerDetected) _state = EnemyStates.MovingToPlayer;
            else _state = EnemyStates.Wandering;
        }

        private void SetBooleans()
        {
            switch (_state)
            {
                case EnemyStates.Dead:
                    _mover.CanMove = false;
                    _mover.IsMovingToPlayer = false;
                    _attacker.CanAttack = false;
                    break;
                case EnemyStates.Wandering:
                    _mover.IsMovingToPlayer = false;
                    _mover.CanMove = true;
                    break;
                case EnemyStates.MovingToPlayer:
                    _mover.IsMovingToPlayer = true;
                    _mover.CanMove = true;
                    break;
                case EnemyStates.Aiming:
                    _nav.velocity = Vector3.zero;
                    _mover.CanMove = true;
                    break;
                case EnemyStates.Attacking:
                    _nav.velocity = Vector3.zero;
                    _mover.CanMove = false;
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
