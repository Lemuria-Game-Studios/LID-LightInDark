using System;
using UnityEngine;
using Signals;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnAppearingInGameUI += OnAppearingInGameUI;
            CoreGameSignals.Instance.OnAppearingMenuUI += OnAppearingMenuUI;
        }
        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnAppearingInGameUI -= OnAppearingInGameUI;
        }

        private void OnAppearingInGameUI()
        {
            Destroy(canvas.transform.GetChild(0).gameObject);
            Instantiate(Resources.Load<GameObject>("UI/InGameUI"), canvas.transform, false);
        }
        private void OnAppearingMenuUI()
        {
            Destroy(canvas.transform.GetChild(0).gameObject);
            Instantiate(Resources.Load<GameObject>("UI/PauseMenu"), canvas.transform, false);
        }
    }
}
