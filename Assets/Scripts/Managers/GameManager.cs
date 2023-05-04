using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStates states;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnChangeGameState += OnChangeGameState;
            CoreGameSignals.Instance.OnPausingGame += OnPausingGame;
            CoreGameSignals.Instance.OnResumingGame += OnResumingGame;
            CoreGameSignals.Instance.OnGettingGameState += OnGettingGameState;
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnChangeGameState -= OnChangeGameState;
            CoreGameSignals.Instance.OnPausingGame -= OnPausingGame;
            CoreGameSignals.Instance.OnResumingGame -= OnResumingGame;
            CoreGameSignals.Instance.OnGettingGameState -= OnGettingGameState;
        }

        private void OnChangeGameState(GameStates state)
        {
            states = state;
        }

        public GameStates OnGettingGameState()
        {
            return states;
        }

        private void OnPausingGame()
        {
            OnChangeGameState(GameStates.Pause);
            Time.timeScale = 0;
        }
        private void OnResumingGame()
        {
            OnChangeGameState(GameStates.Game);
            Time.timeScale = 1;
        }

        
    }
}
