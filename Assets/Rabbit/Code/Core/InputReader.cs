using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static RabbitInputActions;

namespace Rabbit {
    public interface IInputReader
    {
        void EnablePlayerActions();
        void DisablePlayerActions();
    }

    [CreateAssetMenu(fileName = "InputReader", menuName = "Rabbit/InputReader", order = 0)]
    public class InputReader : ScriptableObject, IInputReader, IGameplayActions {

        public event Action OnPausePressed;
        public event Action OnLcmPressed;
        
        public float rotate => _inputActions.Gameplay.Rotate.ReadValue<float>();
        
        RabbitInputActions _inputActions;

        
        public void EnablePlayerActions() {
            if (_inputActions == null)
            {
                _inputActions = new RabbitInputActions();
                _inputActions.Gameplay.SetCallbacks(this);
            }
            _inputActions.Enable();
        }
        
        public void DisablePlayerActions() {
            _inputActions.Disable();
        }
        
        public void OnPause(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                OnPausePressed?.Invoke();
                GameEvents.UI.OnButtonPressed?.Invoke(GC.UI.ButtonTypes.Pause);
            }
        }
        
        public void OnRotate(InputAction.CallbackContext context) {
            
        }
        
        public void OnLMC(InputAction.CallbackContext context) {
            OnLcmPressed?.Invoke();
        }
    }
}