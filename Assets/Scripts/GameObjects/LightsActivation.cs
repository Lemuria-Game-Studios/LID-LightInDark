using System;
using UnityEngine;

namespace GameObjects
{
    public class LightsActivation : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Light"))
            {
                Transform[] childTransforms = other.GetComponentsInChildren<Transform>(true);

                foreach (Transform childTransform in childTransforms)
                {
                    if (childTransform != other.transform) // Kendi transformunu atla
                    {
                        childTransform.gameObject.SetActive(true);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Light"))
            {
                Transform[] childTransforms = other.GetComponentsInChildren<Transform>();

                foreach (Transform childTransform in childTransforms)
                {
                    if (childTransform != other.transform) // Ana objeyi atlayarak sadece child objeleri işleme alıyoruz
                    {
                        childTransform.gameObject.SetActive(false);
                    }
                }
                other.gameObject.SetActive(true); // Ana objenin setActive değerini true olarak ayarlıyoruz
            }
        }
    }
}
