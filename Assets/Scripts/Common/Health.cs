using MoreMountains.Feedbacks;
using UnityEngine;

namespace Common
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        [Header("Weak Point")]
        [SerializeField] private float weakPointDamageAmplification;
        [Range(0, 360)] public float weakPointAngle;
        [Range(0, 360)] public float weakPointPosition;

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks dieFb;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void ChangeHealth(float amount, Vector3 getHitPosition = default(Vector3))
        {
            if (CheckIfWeakPoint(getHitPosition))currentHealth += amount * weakPointDamageAmplification;
            else currentHealth += amount;
            CheckIfDead();
        }

        private bool CheckIfWeakPoint(Vector3 getHitPosition)
        {
            return Vector3.Angle(new Vector3(Mathf.Sin(transform.eulerAngles.y + weakPointPosition * Mathf.Deg2Rad), 0, Mathf.Cos(weakPointPosition * Mathf.Deg2Rad)),
                new Vector3(getHitPosition.x, transform.position.y, getHitPosition.z) - transform.position) < weakPointAngle / 2;
        }

        private void CheckIfDead()
        {
            if (currentHealth <= 0) Die();
        }

        private void Die()
        {
            dieFb.PlayFeedbacks();
            Debug.Log("dead");
        }
    }
}
