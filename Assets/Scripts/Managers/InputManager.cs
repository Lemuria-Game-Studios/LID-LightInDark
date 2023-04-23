using UnityEngine;
using Enums;
using Signals;
using Controllers;
namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PlayerMovementController playerMovementController;
        public Vector3 input;

        private void Update()
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
                    PlayerSignals.Instance.OnAttacking?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    PlayerSignals.Instance.OnSpelling?.Invoke();
                }

                if (Input.GetKeyDown((KeyCode.Space)) )
                {
                    PlayerSignals.Instance.CanDash?.Invoke();
                }
            }
            else
            {
                
            }
        }
        private void GatherInput() {
            if (gameManager.GetGameState() == GameStates.Game && !playerMovementController.GetIsDashing() )
            {
                input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            }
            
        }
    }
}
