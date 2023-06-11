using System;
using UnityEngine;

namespace GameObjects
{
    public class StageActivation : MonoBehaviour
    {
        [SerializeField] private GameObject stage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                stage.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
