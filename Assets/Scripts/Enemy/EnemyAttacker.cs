using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttacker : MonoBehaviour
    {
        public float AttackRange;
        [Range(0, 360)] public float AttackAngle;
        [SerializeField] [Range(0,10)] private float rotateSpeedInRange;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] [Min(0f)] private float attackCooldown = 0.5f;
        
        private bool _canAttack = true;

        [Header("FeedBacks")] 
        [SerializeField] private List<MMFeedbacks> attackFeedbackList;

        [HideInInspector] public bool IsInRange;
        [HideInInspector] public bool IsAttacking;

        [HideInInspector] public Transform Target;

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
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);
            IsInRange = rangeChecks.Length != 0 && rangeChecks[0].transform == Target;
        }
        
        public void Attack()
        {
            if (IsAttacking || !_canAttack) return;
            RotateTowardsTarget();
            if (Vector3.Angle(transform.forward, Target.position - transform.position) > AttackAngle/2) return;
            IsAttacking = true;
            _canAttack = false;
            attackFeedbackList[Random.Range(0, attackFeedbackList.Count - 1)].PlayFeedbacks();
        }
        private void RotateTowardsTarget()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotateSpeedInRange);
        }

        private void SetAttackParams()
        {
            IsAttacking = false;
            StartCoroutine(SetCanAttackTrueAfterDelay());
        }

        private IEnumerator SetCanAttackTrueAfterDelay()
        {
            yield return new WaitForSeconds(attackCooldown);
            _canAttack = true;
        }
    }
}
