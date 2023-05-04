using UnityEngine;
using Signals;
using System.Threading.Tasks;
using Enums;

namespace Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange =0.5f;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private int attackTime;
        [SerializeField] private bool canAttack=true;

        private void Awake()
        {
            attackTime = 1000;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.OnSwordAttack += OnSwordAttack;
            InputSignals.Instance.OnArchering += OnArchering;
        }

        private async void OnSwordAttack()
        {
            if (canAttack)
            {
                canAttack = false;
                AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.CloseAttack);
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider enemies in hitEnemies)
                {
                    Debug.Log("Hit");
                }
                await Task.Delay(attackTime);
                canAttack = true;
            }
        }

        private async void OnArchering()
        {
            if (canAttack)
            {
                canAttack = false;
                AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.RangeAttack);
                Instantiate(Resources.Load<GameObject>("Arrows/Arrow"),
                    attackPoint.position,attackPoint.rotation);
                await Task.Delay(attackTime);
                canAttack = true;
            }
            
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            InputSignals.Instance.OnSwordAttack -= OnSwordAttack;
            InputSignals.Instance.OnArchering -= OnArchering;
        }
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }
            Gizmos.DrawSphere(attackPoint.position,attackRange);
        }
    }
}
