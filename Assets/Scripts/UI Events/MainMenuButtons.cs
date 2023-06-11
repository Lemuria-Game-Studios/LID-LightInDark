using UnityEngine;
using Signals;
using Enums;

namespace UI_Events
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void StartButton()
        {
            if (!ES3.KeyExists("AttackLevel")) {
                CoreGameSignals.Instance.OnLoadingScene?.Invoke("SampleScene");
            }
            else
            {
                Debug.Log("Yoek");
            }
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
        
    }
}
