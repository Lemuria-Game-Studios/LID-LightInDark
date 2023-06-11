using Enums;
using Managers;
using UnityEngine;

namespace GameObjects
{
    public class PoisonBall : MonoBehaviour
    {
     
        private void OnEnable()
        {
            Destroy(gameObject,5);
            var transform1 = transform;
            Vector3 rotasyon = transform1.eulerAngles;
            rotasyon.x = -90f;
            transform1.eulerAngles = rotasyon;
        }

        private void Update()
        {
            Movement();
            
        }

        private void Movement()
        {
            transform.Translate(0,0,10f*Time.deltaTime);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            var collidedObjectLayer = other.gameObject.layer;
            if (LayerMask.LayerToName(collidedObjectLayer) == "Player")
            {
                Debug.Log("PoisonBall");
                other.gameObject.GetComponent<PlayerManager>().ChangingHealth(HealthOperations.Attack,30);
                Destroy(gameObject);
            }
            
        }
    }
}
