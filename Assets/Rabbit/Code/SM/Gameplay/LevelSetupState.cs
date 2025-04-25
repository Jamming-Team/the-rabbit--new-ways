using UnityEngine;

namespace Rabbit.Gameplay {
    public class LevelSetupState : State<ActionState> {

        [SerializeField] Transform _levelRoot;
        
        protected override void OnEnter() {
            base.OnEnter();

            Instantiate((_core.stateData as LevelSetupStateData).levelPrefab, _levelRoot);
            
            _core.DecideOnNextAction_State();
        }
        

    }
}