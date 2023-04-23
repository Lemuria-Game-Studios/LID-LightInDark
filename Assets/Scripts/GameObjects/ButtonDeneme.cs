using Managers;
using UnityEngine;
using Signals;

namespace GameObjects
{
    public class ButtonDeneme : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
            Debug.Log("Button");
        }
    }
}
