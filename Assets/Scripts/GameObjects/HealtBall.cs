using Managers;
using UnityEngine;
using Enums;

namespace GameObjects
{
    public class HealtBall : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerManager>().ChangingHealth(HealthOperations.Heal,50);
                Destroy(gameObject);
            }
        }
    }
}
