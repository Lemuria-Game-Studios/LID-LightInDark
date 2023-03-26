using System;
using UnityEngine;
using Enums;
using UnityEngine.Serialization;
using Signals;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        public Vector3 input;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }


        void Update()
        {
            PlayerInputs();
            GatherInput();
        }
        private void PlayerInputs()
        {
            if (gameManager.GetGameState() == GameStates.Game)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlayerSignals.Instance.onAttacking?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    PlayerSignals.Instance.onSpelling?.Invoke();
                }

                if (Input.GetKeyDown((KeyCode.Space)))
                {
                    PlayerSignals.Instance.onDashing?.Invoke();
                }
            }
            else
            {
                
            }
        }
        private void GatherInput() {
            if (gameManager.GetGameState() == GameStates.Game)
            {
                input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            }
            
        }
    }
}
