using UnityEngine;

namespace GameObjects
{
    public class Arrow : MonoBehaviour
    {
        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            transform.Translate(0,0,0.01f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Arrow");
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.gameObject.transform.forward*5f,ForceMode.Impulse);
            collision.gameObject.transform.Translate(collision.gameObject.transform.forward*0.5f);
            Destroy(gameObject);
        }
    }
}
