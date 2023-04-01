using MoreMountains.Feedbacks;
using UnityEngine;

namespace Common
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        
        public bool WeakPointEnabled;
        [SerializeField] private float weakPointDamageAmplification;
        [Range(0, 360)] public float WeakPointAngle;
        [Range(0, 360)] public float WeakPointPosition;
        
        [SerializeField] private MMFeedbacks getHitFb;
        [SerializeField] private MMFeedbacks dieFb;
        
        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void ChangeHealth(float amount, Vector3 getHitPosition = default)
        {
            if (amount < 0) getHitFb.PlayFeedbacks();
            if (WeakPointEnabled && CheckIfWeakPoint(getHitPosition))_currentHealth += amount * weakPointDamageAmplification;
            else _currentHealth += amount;
            CheckIfMaxHealthReached();
            CheckIfDead();
        }

        private bool CheckIfWeakPoint(Vector3 getHitPosition)
        {
            if (getHitPosition == default) return false;
            return Vector3.Angle(new Vector3(Mathf.Sin(transform.eulerAngles.y + WeakPointPosition * Mathf.Deg2Rad), 0, Mathf.Cos(WeakPointPosition * Mathf.Deg2Rad)),
                new Vector3(getHitPosition.x, transform.position.y, getHitPosition.z) - transform.position) < WeakPointAngle / 2;
        }

        private void CheckIfMaxHealthReached()
        {
            if (_currentHealth > maxHealth) _currentHealth = maxHealth;
        }
        
        private void CheckIfDead()
        {
            if (_currentHealth <= 0) Die();
        }

        private void Die()
        {
            dieFb.PlayFeedbacks();
            Debug.Log("dead");
        }
    }
}
