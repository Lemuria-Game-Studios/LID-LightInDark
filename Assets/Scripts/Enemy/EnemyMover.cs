using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        public Vector3 wanderCenterPosition;
        public float wanderRange;
        public bool isMovingToPlayer;

        private bool _isDestinationSet;

        private NavMeshAgent _nav;

        public bool canMove = true;
        [HideInInspector] public Transform target;

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
        }

        private void FixedUpdate()
        {
            SetDestination();
            SetIfCanMove();
            CheckIfReached();
        }

        private void SetDestination()
        {
            if (!canMove) return;
            
            if (isMovingToPlayer)
            {
                _isDestinationSet = true;
                _nav.destination = target.transform.position;
            }
            else if (!_isDestinationSet)
            {
                _isDestinationSet = true;
                var randomPos = Random.insideUnitSphere * wanderRange + wanderCenterPosition;
                _nav.destination = new Vector3(randomPos.x, transform.position.y, randomPos.z);
                if (NavMesh.SamplePosition(_nav.destination, out NavMeshHit hit, 20, NavMesh.GetAreaFromName("Walkable")))
                {
                    _nav.destination = new Vector3(hit.position.x, transform.position.y, hit.position.z);
                }
            }
        }
        
        private void SetIfCanMove()
        {
            _nav.isStopped = !canMove;
            if (!canMove) GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        }
        
        private void CheckIfReached()
        {
            if (Vector3.Distance(_nav.destination, transform.position) < 1f || _nav.isStopped)
            {
                StartCoroutine(SetIsDestinationSetFalseAfterDelay());
            }
        }
        
        private IEnumerator SetIsDestinationSetFalseAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            _isDestinationSet = false;
            StopAllCoroutines();
        }
    }
}
