using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSavingGame += OnSavingGame;
            CoreGameSignals.Instance.OnLoadingGame += OnLoadingGame;
        }

        private void OnSavingGame()
        {
            ES3.Save("transform",PlayerSignals.Instance.OnGettingTransform.Invoke());
            ES3.Save("dashMeter",PlayerSignals.Instance.OnGettingDashMeter.Invoke());
        }
        
        private void OnLoadingGame()
        {
            ES3.Load<Transform>("transform", PlayerSignals.Instance.OnGettingTransform.Invoke());
            PlayerSignals.Instance.OnSettingDashMeter(ES3.Load<float>("dashMeter"));
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnSavingGame -= OnSavingGame;
            CoreGameSignals.Instance.OnLoadingGame -= OnLoadingGame;
        }
    }
}
