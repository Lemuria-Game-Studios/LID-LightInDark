using Managers;
using UnityEngine;
using Signals;

namespace GameObjects
{
    public class ButtonDeneme : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            CoreGameSignals.Instance.OnAppearingInGameUI?.Invoke();
            Debug.Log("Button");
        }
    }
}
