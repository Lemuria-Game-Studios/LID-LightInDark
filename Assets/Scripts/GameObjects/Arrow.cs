using Common;
using UnityEngine;

namespace GameObjects
{
    public class Arrow : MonoBehaviour
    {
        
        private void OnEnable()
        {
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
            transform.Translate(0,-0.01f,0);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            var collidedObjectLayer = other.gameObject.layer;
            if (LayerMask.LayerToName(collidedObjectLayer) == "Enemy")
            {
                Debug.Log("Arrow");
                other.gameObject.GetComponent<Health>().Push(transform.position);
                other.gameObject.transform.Translate(other.gameObject.transform.forward*0.5f);
                Destroy(gameObject);
            }
            
        }
    }
}
