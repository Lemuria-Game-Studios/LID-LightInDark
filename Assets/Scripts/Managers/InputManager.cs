using UnityEngine;
using Enums;
using Signals;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        
        public Vector3 input;

        /*private void OnEnable()
        {
            //SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }*/

        private void Update()
        {
            PlayerInputs();
            //GatherInput();
        }
        private void PlayerInputs()
        {
            if (CoreGameSignals.Instance.OnGettingGameState.Invoke() == GameStates.Game)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    InputSignals.Instance.OnSwordAttack?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    InputSignals.Instance.OnArchering?.Invoke();
                }

                if (Input.GetKeyDown((KeyCode.Space)) )
                {
                    InputSignals.Instance.CanDash?.Invoke();
                }
            }
            else
            {
                
            }
        }
        /*private void GatherInput() {
            if (CoreGameSignals.Instance.OnGettingGameState.Invoke() == GameStates.Game && !InputSignals.Instance.OnGetIsDashing.Invoke() )
            {
                input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            }
            
        }*/
    }
}
