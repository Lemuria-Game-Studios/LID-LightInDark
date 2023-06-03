using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyAttacker : MonoBehaviour
    {
        public float AttackRange;
        [Range(0, 360)] public float AttackAngle;
        private Animator _animator;
        [SerializeField] [Range(0,10)] private float rotateSpeedInRange;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] [Min(0f)] private float attackCooldown = 0.5f;
        
        public bool CanAttack = true;

        [Header("FeedBacks")] 
        [SerializeField] private List<MMFeedbacks> attackFeedbackList;

        [HideInInspector] public bool IsInRange;
        [HideInInspector] public bool IsAttacking;

        [HideInInspector] public Transform Target;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

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
            if (IsAttacking || !CanAttack) return;
            RotateTowardsTarget();
            if (Vector3.Angle(transform.forward, Target.position - transform.position) > AttackAngle/2) return;
            IsAttacking = true;
            CanAttack = false;
            attackFeedbackList[Random.Range(0, attackFeedbackList.Count - 1)].PlayFeedbacks();
            StartCoroutine(SetAttackParams());
        }
        private void RotateTowardsTarget()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.deltaTime * rotateSpeedInRange);
        }
        
        

        private IEnumerator SetAttackParams()
        {
            yield return new WaitForSeconds(2);
            IsAttacking = false;
            StartCoroutine(SetCanAttackTrueAfterDelay());
        }
        

        private IEnumerator SetCanAttackTrueAfterDelay()
        {
            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
        }
    }
}
