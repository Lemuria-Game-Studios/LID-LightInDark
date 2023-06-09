using Common;
using UnityEngine;

namespace GameObjects
{
    public class Arrow : MonoBehaviour
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
            transform.Translate(0,-10f*Time.deltaTime,0);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            var collidedObjectLayer = other.gameObject.layer;
            if (LayerMask.LayerToName(collidedObjectLayer) == "Enemy")
            {
                Debug.Log("Arrow");
                other.gameObject.GetComponent<Health>().Push(transform.position);
                other.gameObject.GetComponent<Health>().ChangeHealth(20,transform.position);
                other.gameObject.transform.Translate(other.gameObject.transform.forward*0.5f);
                Destroy(gameObject);
            }
            
        }
    }
}
