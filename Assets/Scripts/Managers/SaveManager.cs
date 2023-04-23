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
            ES3.Save("transform",player.transform);
            ES3.Save("dashMeter",player.gameObject.GetComponent<PlayerManager>().GetDashMeter());
        }
        
        private void OnLoadingGame()
        {
            ES3.Load<Transform>("transform", player.transform);
            player.gameObject.GetComponent<PlayerManager>().SetDashMeter(ES3.Load<float>("dashMeter"));
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
