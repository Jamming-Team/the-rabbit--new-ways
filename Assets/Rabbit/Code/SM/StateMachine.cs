using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rabbit {
    public class StateMachine : MonoBehaviour {
        readonly List<IState> _states = new();
        public IState currentState { get; private set; }


        public void Init(MonoBehaviour core, bool autoSetInitialState) {
            GetComponentsInChildren(_states);
            _states.ForEach(x => {
                x.OnTransitionRequired += ChangeState;
                x.Init(core);
            });
            if (autoSetInitialState)
                ChangeState(_states[0].GetType());
        }

        public void UpdateSM(float delta) {
            currentState.UpdateState(delta);
        }


        public void OnDestroy() {
            _states.ForEach(x => { x.OnTransitionRequired -= ChangeState; });
            currentState.Exit();
        }

        public void ChangeState(Type nextStateType) {
            var nextState = _states.Find(x => x.GetType() == nextStateType);

            if (nextState != null && !Equals(currentState, nextState)) {
                if (currentState != null) currentState.Exit();
                nextState.Enter();
                currentState = nextState;
            }
        }
    }
}