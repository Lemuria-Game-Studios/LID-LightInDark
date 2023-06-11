using Signals;
using UnityEngine;
using Enums;

namespace Managers
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnMainMenuUIManagement += OnMainMenuUIManagement;
        }

        private void OnMainMenuUIManagement(MainMenuStates state)
        {
            Destroy(canvas.transform.GetChild(0).gameObject);
            
            switch (state)
            {
                case MainMenuStates.Credits:
                    Instantiate(Resources.Load<GameObject>("UI/Credits Screen"), canvas.transform, false);
                    break;
                case MainMenuStates.Menu:
                    Instantiate(Resources.Load<GameObject>("UI/Menu Screen"), canvas.transform, false);
                    break;
                case MainMenuStates.Options:
                    Instantiate(Resources.Load<GameObject>("UI/Menu-Options Screen"), canvas.transform, false);
                    break;
            }
        }
    }
}
