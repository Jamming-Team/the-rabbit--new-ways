using System;
using UnityEngine;

namespace Rabbit.Gameplay {
    public class GameplayState : State<ActionState> {

        [SerializeField] GameObject _view;
        
        float timer;

        void Awake() {
            _view.SetActive(false);
        }

        protected override void OnEnter() {
            base.OnEnter();

            BatteryManager.Instance.HasAvailableBattery();
            timer = 0;
            _view.SetActive(true);
        }
        
        protected override void OnExit() {
            base.OnExit();
            
            if (_view)
                _view.SetActive(false);
        }

        public override void UpdateState(float delta) {
            
            _core.currentTime += delta;
            timer += delta;

            if (timer >= ((GameplayStateData)_core.stateData).timeTillNextAction) {
                _core.DecideOnNextAction_State();
                return;
            }
            
            GameEvents.Gameplay.OnGameplayUpdate?.Invoke(delta);
        }
        
    }
}