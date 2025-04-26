using System.Collections;
using UnityEngine;

namespace Rabbit.Gameplay {
    public class DoStuffState : State<ActionState> {

        int difficultyLvl = -1;
        
        protected override void OnEnter() {
            base.OnEnter();
            
            StartCoroutine(DoStuff());
        }

        protected override void OnExit() {
            base.OnExit();
        }
        
        IEnumerator DoStuff() {
            yield return new WaitForSeconds(0.1f);
            
            switch ((_core.stateData as DoStuffStateData).stuffType) {
                case DoStuffTypes.UpDifficulty: {
                    difficultyLvl++;
                    GameEvents.Gameplay.OnChangeDifficultySet?
                        .Invoke(_core.core.data.difficulties[difficultyLvl]);
                    break;
                }
            }
            
            _core.DecideOnNextAction_State();
        }
    }
}