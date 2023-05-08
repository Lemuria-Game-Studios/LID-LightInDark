using System;
using UnityEngine;
using Enums;
using Signals;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private float horizontalMovement;
        private float verticalMovement;

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

        private void FixedUpdate()
        {
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

                if (Input.GetKeyDown((KeyCode.Space))&& !InputSignals.Instance.OnGetCanDash.Invoke())
                {
                    InputSignals.Instance.CanDash?.Invoke();
                    
                }
            }
            else
            {
                
            }
        }
        /*private void GatherInput() {
            if (CoreGameSignals.Instance.OnGettingGameState.Invoke() == GameStates.Game)
            {
                horizontalMovement = Input.GetAxisRaw("Horizontal"); 
                verticalMovement = Input.GetAxisRaw("Vertical");

                InputSignals.Instance.OnMovementAndRotation.Invoke(horizontalMovement, verticalMovement);
            }
            
        }*/
    }
}
