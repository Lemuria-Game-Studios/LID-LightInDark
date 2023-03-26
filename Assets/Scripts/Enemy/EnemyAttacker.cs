using System.Collections;
using MoreMountains.Feedbacks;
using Unity.Mathematics;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttacker : MonoBehaviour
    {
        public float attackRange;
        [Range(0, 360)] public float attackAngle;
        [SerializeField] [Range(0,10)] private float rotateSpeedInRange;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] [Min(0.5f)] private float delayBetweenAttacks = 0.5f;
        
        public bool isInRange;
        public bool isAttacking;
        private bool _canAttack = true;

        [Header("FeedBacks")] 
        [SerializeField] private MMFeedbacks attackFb;


        [HideInInspector] public Transform target;

        private void Start()
        {
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                CheckFOV();
            }
        }

        private void CheckFOV()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
            isInRange = rangeChecks.Length != 0 && rangeChecks[0].transform == target;
        }
        
        public void Attack()
        {
            if (isAttacking || !_canAttack) return;
            RotateTowardsTarget();
            if (Vector3.Angle(transform.forward, target.position - transform.position) > attackAngle/2) return;
            isAttacking = true;
            _canAttack = false;
            attackFb.PlayFeedbacks();
        }
        private void RotateTowardsTarget()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * rotateSpeedInRange);
        }

        private void SetAttackParams()
        {
            isAttacking = false;
            StartCoroutine(SetCanAttackTrueAfterDelay());
        }

        private IEnumerator SetCanAttackTrueAfterDelay()
        {
            yield return new WaitForSeconds(delayBetweenAttacks);
            _canAttack = true;
        }
    }
}
