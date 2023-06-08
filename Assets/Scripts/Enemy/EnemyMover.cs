using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        public Vector3 WanderCenterPosition;
        public float WanderRange;
        public bool IsMovingToPlayer;

        [SerializeField] private bool _isDestinationSet;

        [SerializeField]private NavMeshAgent _nav;

        public bool CanMove = true;
        [HideInInspector] public Transform target;
        private Animator _animator;
        private static readonly int İsWalking = Animator.StringToHash("isWalking");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            WanderCenterPosition = transform.position;
        }

        private void FixedUpdate()
        {
            SetDestination();
            SetIfCanMove();
            CheckIfReached();
        }

        private void SetDestination()
        {
            if (IsMovingToPlayer)
            {
                _isDestinationSet = true;
                _nav.destination = target.transform.position;
                _animator.SetBool(İsWalking, true);
            }
            else if (!_isDestinationSet)
            {
                _isDestinationSet = true;
                var randomPos = Random.insideUnitSphere * WanderRange + WanderCenterPosition;
                _nav.destination = new Vector3(randomPos.x, transform.position.y, randomPos.z);
                if (NavMesh.SamplePosition(_nav.destination, out NavMeshHit hit, 20, NavMesh.GetAreaFromName("Walkable")))
                {
                    _nav.destination = new Vector3(hit.position.x, transform.position.y, hit.position.z);
                }
            }
        }
        
        private void SetIfCanMove()
        {
            _nav.isStopped = !CanMove;
            if (!CanMove)
            {
                _animator.SetBool(İsWalking, false);
            }
        }
        
        private void CheckIfReached()
        {
            if (Vector3.Distance(_nav.destination, transform.position) < 1f)
            {
                _animator.SetBool(İsWalking, false);
                StartCoroutine(SetIsDestinationSetFalseAfterDelay());
            }
        }
        
        private IEnumerator SetIsDestinationSetFalseAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            _animator.SetBool(İsWalking, true);
            _isDestinationSet = false;
            StopAllCoroutines();
        }
    }
}
