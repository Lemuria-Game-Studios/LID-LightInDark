using System.Threading.Tasks;
using Enums;
using MoreMountains.Feedbacks;
using UnityEngine;
using Signals;
using Enemy;
using UI;

namespace Common
{
    public class Health : MonoBehaviour
    {
        public bool isDead;
        [SerializeField] private ushort profit;
        [SerializeField] private float maxHealth;
        
        public bool WeakPointEnabled;
        [SerializeField] private float currentHealth;
        [SerializeField] private float weakPointDamageAmplification;
        [Range(0, 360)] public float WeakPointAngle;
        [Range(0, 360)] public float WeakPointPosition;
        
        [SerializeField] private Canvas healthBarCanvas;
        
        
        [SerializeField] private MMFeedbacks getHitFb;
        [SerializeField] private MMFeedbacks dieFb;
        private Rigidbody _rigidbody;
        
        

        private void Awake()
        {
            currentHealth = maxHealth;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ChangeHealth(float amount, Vector3 getHitPosition = default)
        {
            if (amount < 0) getHitFb.PlayFeedbacks();
            if (WeakPointEnabled && CheckIfWeakPoint(getHitPosition))currentHealth -= amount * weakPointDamageAmplification;
            else currentHealth -= amount;
            healthBarCanvas.gameObject.SetActive(true);
            healthBarCanvas.GetComponent<HealthBar>().UptadeHealthBar(maxHealth,currentHealth);
            CheckIfMaxHealthReached();
            Debug.Log(currentHealth);
            CheckIfDead();
        }

        private bool CheckIfWeakPoint(Vector3 getHitPosition)
        {
            if (getHitPosition == default) return false;
            return Vector3.Angle(new Vector3(Mathf.Sin(transform.eulerAngles.y + WeakPointPosition * Mathf.Deg2Rad), 0, Mathf.Cos(WeakPointPosition * Mathf.Deg2Rad)),
                new Vector3(getHitPosition.x, transform.position.y, getHitPosition.z) - transform.position) < WeakPointAngle / 2;
        }

        public async void Push(Vector3 playerPosition)
        {
            _rigidbody.isKinematic = false;
            gameObject.GetComponent<EnemyMover>().CanMove = false;
            Vector3 pushRotation = playerPosition - transform.position;
            _rigidbody.AddForce(-pushRotation.normalized * 30, ForceMode.Impulse);
            await Task.Delay(300);
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.isKinematic = true;
                gameObject.GetComponent<EnemyMover>().CanMove = true;
            }
            
        }

        private void CheckIfMaxHealthReached()
        {
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }
        
        private void CheckIfDead()
        {
            if (currentHealth <= 0) Die();
        }

        private void Die()
        {
            if(isDead)return;
            CoreGameSignals.Instance.OnSettingMoney(MoneyOperations.Earn, profit);
            dieFb.PlayFeedbacks();
            Debug.Log("dead");
            isDead = true;
        }
    }
}
