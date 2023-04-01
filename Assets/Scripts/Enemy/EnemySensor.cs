using System.Collections;
using UnityEngine;

namespace Enemy
{
     public class EnemySensor : MonoBehaviour
     {
          public float SightRange;
          [Range(0,360)] public float SightAngle;
          public float SensoryRange;
          [SerializeField] private LayerMask targetLayer;
          [SerializeField] private LayerMask obstructionLayer;
          
          [HideInInspector] public bool IsPlayerDetected;
          [HideInInspector] public Transform Target;
          
          private EnemyController _controller;

          private void Awake()
          {
               _controller = GetComponent<EnemyController>();
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
               Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Mathf.Max(SightRange, SensoryRange), targetLayer);

               if (rangeChecks.Length != 0)
               {
                    Target = rangeChecks[0].transform;
                    Vector3 directionToTarget = (Target.position - transform.position).normalized;
                    float distanceToTarget = Vector3.Distance(transform.position, Target.position);
               
                    //sensoryFOV
                    if (distanceToTarget <= SensoryRange)
                    {
                         //none obstruction if it senses
                         IsPlayerDetected = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, 0);
                    }
                    //sightFOV
                    else if (Vector3.Angle(transform.forward, directionToTarget) < SightAngle / 2 && distanceToTarget <= SightRange)
                    {
                         //obstruction exists even it sees
                         IsPlayerDetected = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer);
                    }
                    else IsPlayerDetected = false;
               }
               else if (IsPlayerDetected)
                    IsPlayerDetected = false;
          }
     }
}
