using System;
using UnityEngine;
using Signals;
using Enums;
using UnityEngine.UI;

namespace UI_Events
{
    public class InGameButtonEvents : MonoBehaviour
    {
        

        public void SaveButton()
        {
            CoreGameSignals.Instance.OnSavingGame?.Invoke();
        }

        public void LoadButton()
        {
            CoreGameSignals.Instance.OnLoadingGame?.Invoke();
        }

        public void MenuButton()
        {
            CoreGameSignals.Instance.OnPausingGame?.Invoke();
            CoreGameSignals.Instance.OnUIManagement?.Invoke(GameStates.Pause);
        }
        public void ResumeButton()
        {
            CoreGameSignals.Instance.OnUIManagement?.Invoke(GameStates.Game);
            CoreGameSignals.Instance.OnResumingGame?.Invoke();
        }

        


    }
}
