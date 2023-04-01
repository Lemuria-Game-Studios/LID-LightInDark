using Common;
using UnityEngine;

namespace Enemy
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private float damageAmount;
        [SerializeField] private LayerMask targetLayer;
        
        private void DealDamage()
        {
            var pos = transform.position;
            Collider[] rangeChecks = Physics.OverlapCapsule(new Vector3(pos.x, pos.y - 1, pos.z),
                new Vector3(pos.x, pos.y + 1, pos.z), transform.localScale.x/2, targetLayer);
            
            if (rangeChecks.Length > 0)
            {
                rangeChecks[0].GetComponent<Health>().ChangeHealth(-damageAmount, transform.position);
            }
            
            Destroy(gameObject);
        }
    }
}
