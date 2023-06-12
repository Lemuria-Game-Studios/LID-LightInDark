using UnityEngine;
using Signals;
using Enums;

namespace UI_Events
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void StartButton()
        {
            CoreGameSignals.Instance.OnLoadingScene?.Invoke("SampleScene");
        }

        public void OptionsButton()
        {
            CoreGameSignals.Instance.OnMainMenuUIManagement.Invoke(MainMenuStates.Options);
        }

        public void BackButton()
        {
            CoreGameSignals.Instance.OnMainMenuUIManagement.Invoke(MainMenuStates.Menu);
        }

        public void CreditsButton()
        {
            CoreGameSignals.Instance.OnMainMenuUIManagement.Invoke(MainMenuStates.Credits);
        }
        
        public void QuitButton()
        {
            Application.Quit();
        }
        
    }
}
