using UnityEngine;
using Signals;
using Enums;
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
            CoreGameSignals.Instance.OnAppearingMenuUI?.Invoke();
            CoreGameSignals.Instance.OnPausingGame?.Invoke();
        }
        public void ResumeButton()
        {
            CoreGameSignals.Instance.OnAppearingInGameUI?.Invoke();
            CoreGameSignals.Instance.OnResumingGame?.Invoke();
        }
    }
}
