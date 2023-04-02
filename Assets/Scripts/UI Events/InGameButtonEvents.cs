using UnityEngine;
using Signals;
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
    }
}
