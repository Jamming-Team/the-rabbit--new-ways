using System.Collections.Generic;
using UnityEngine;

namespace Rabbit.Gameplay.InterfaceState {
    public class InterfaceState : State<GP_SceneController> {
        
        [SerializeField] StateMachine _stateMachine;
        
        public override void Init(MonoBehaviour core) {
            _core = (GP_SceneController)core;
            
            _stateMachine.Init(this, false);

        }
        
        protected override void OnEnter() {
            base.OnEnter();
            Time.timeScale = 0;
            _stateMachine.ChangeState(_core.interfaceType);
            GameEvents.UI.OnButtonPressed += InputReaderOnOnPausePressed;
        }
        
        protected override void OnExit() {
            base.OnExit();
            Time.timeScale = 1;
            _stateMachine.ChangeState(typeof(NoneState));
            GameEvents.UI.OnButtonPressed -= InputReaderOnOnPausePressed;
        }

        void InputReaderOnOnPausePressed(GC.UI.ButtonTypes type) {
            if (type != GC.UI.ButtonTypes.Pause)
                return;
            RequestTransition<ActionState>();
        }
        
    }
}