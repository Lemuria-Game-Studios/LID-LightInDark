using UnityEngine;
using Signals;
using System.Threading.Tasks;
using Common;
using Enums;

namespace Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange =0.5f;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private ushort attackTime;
        [SerializeField] private bool canAttack=true;
        [SerializeField] private ParticleSystem swordTrail;
        
        private void Awake()
        {
            attackTime = PlayerSignals.Instance.OnGettingAttackSpeed.Invoke();
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

        private async void OnSwordAttack(AttackCombo combo)
        {
            if (canAttack)
            {
                canAttack = false;
                swordTrail.Play();
                switch (combo)
                {
                    case AttackCombo.Attack1:
                        AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.CloseAttack);
                        Debug.Log("Normal");
                        break;
                    case AttackCombo.Attack2:
                        AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Attack2);
                        Debug.Log("Combo");
                        break;
                }
                
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider enemies in hitEnemies)
                {
                    Debug.Log("Hit");
                    enemies.gameObject.GetComponent<Health>().ChangeHealth(PlayerSignals.Instance.OnGettingAttackPower(),gameObject.transform.position);
                    enemies.gameObject.GetComponent<Health>().Push(transform.position);
                }

                WaitForAttack();

            }
        }

        private async Task WaitForAttack()
        {
            await Task.Delay(1000);
            swordTrail.Stop();
            canAttack = true;
        }

        private async void OnArchering()
        {
            if (canAttack)
            {
                canAttack = false;
                AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.RangeAttack);
                await Task.Delay(700);
                Instantiate(Resources.Load<GameObject>("Arrows/Ok"),
                    attackPoint.position,attackPoint.rotation);
                await Task.Delay(PlayerSignals.Instance.OnGettingAttackSpeed.Invoke());
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
